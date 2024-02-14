namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;

//AFTER APDATE
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {

        if (Factory.Get().Schedule.GetStage() != BO.Stage.planning)  // Make sure the project is in the planning stage
            throw new BO.BlNotFitSchedule("Can not add tasks after Project planning phase");

        DO.Task doTask = new DO.Task(boTask.TaskId, boTask.NickName, boTask.Description, DateTime.Now) with { RequiredLevel = (DO.EngineerLevel)boTask.RequiredLevel, NumOfDays = boTask.NumOfDays }; //?
        try
        {
            if (boTask.TaskId < 0)
                throw new BO.BlNotVaildException("id is not valid");

            if (boTask.NickName == "")
                throw new BO.BlNotVaildException("NickName is not valid");

            int idTsk = _dal.Task.Create(doTask); //Create in the data layer

            if (boTask.Dependencies != null)  //if the task depends in ather task 
            {
                foreach (BO.TaskInList item in boTask.Dependencies)
                {
                    DO.Dependence dependence = new DO.Dependence(idTsk, boTask.TaskId, item.TaskId);
                    _dal.Dependence.Create(dependence);
                }
            }

            return idTsk;   // Return the new task ID
        }
        catch (DO.DalAlreadyExistsException ex) //will catch an exception that will be thrown from the DAL layer if a task with the same ID already exists
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.TaskId} already exists", ex);
        }

    }

    public void Delete(int id)
    {

        if (Factory.Get().Schedule.GetStage() != BO.Stage.planning)  // Make sure the project is in the planning stage
            throw new BO.BlNotFitSchedule("Can not delete tasks after Project planning phase");


        DO.Dependence tempDp = _dal.Dependence.Read(item => item.PreviousTaskId == id); //checking if there is another task that depended in this task
        if (tempDp != null)
            throw new BO.BlDeletionImpossible("The task cannot be deleted because there is a task that depends on it");

        try
        {
            DO.Task tempTsk = _dal.Task.Read(id);
            if (tempTsk == null) throw new BO.BlDoesNotExistException($"Task with ID=[{id}] does Not exist");
        
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }

    public IEnumerable<TaskInList> ReadAll(Func<BO.Task, bool>? predicate = null)
    {
        if(predicate == null)
        {
            IEnumerable<BO.TaskInList> tasks = (from DO.Task item in _dal.Task.ReadAll(null)
                                                select new BO.TaskInList()
                                                {
                                                    TaskId = item.TaskId,
                                                    NickName = item.NickName,
                                                    Description = item.Description
                                                }) ;
            return tasks;
        }
        else
        {
          IEnumerable<BO.TaskInList> tasks1 = (from DO.Task item in _dal.Task.ReadAll(null)
                                                        where predicate()
                                                        select new BO.TaskInList()
                                                        {
                                                            TaskId = item.TaskId,
                                                            NickName = item.NickName,
                                                            Description = item.Description
                                                        });
            return tasks1;
        }
          


       
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");

        BO.Task task = new BO.Task()
        {
            TaskId = id,
            Description = doTask.Description,
            NickName = doTask.NickName,
            CreationDate = doTask.CreationDate,
            EstimatedDate = doTask.EstimatedDate,
            StartDate = doTask.StartDate,
            NumOfDays = doTask.NumOfDays ?? 0, // Convert TimeForTask to NumOfDays
            DeadLine = doTask.DeadLine,
            FinishtDate = doTask.FinishtDate,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
            EngineerId = (int)doTask.EngineerId,
            RequiredLevel = (BO.EngineerLevel)doTask.RequiredLevel
        };

        if (doTask.EngineerId != 0) //information about the engineer that work on the task
        {
            task.Engineer = new BO.EngineerInTask()
            {
                Id = (int)doTask.EngineerId,
                Name = (_dal.Engineer.Read(id) ??
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist")).Name

            };
        }

        task.DeadLine = getPlanToFinish(doTask);
        task.Dependencies = getLinks(task);

        return task;
    }

    private List<BO.TaskInList> getLinks(BO.Task task)
    {
        List<DO.Dependence> dep = new List<DO.Dependence>(_dal.Dependence.ReadAll(link => link.PendingTaskId == task.TaskId));
        if (dep.Count == 0)
            return new List<BO.TaskInList>();
        List<BO.TaskInList> tasks = new List<BO.TaskInList>();

        foreach (DO.Dependence d in dep)
        {
            DO.Task doTask = _dal.Task.Read(d!.PreviousTaskId) ?? throw new BO.BlDoesNotExistException("");
            BO.TaskInList newTask = new BO.TaskInList
            {
                TaskId = doTask.TaskId,
                NickName = doTask.NickName,
                Description = doTask.Description
            };
            tasks.Add(newTask);
        }
        return tasks;
    }
    private DateTime? getPlanToFinish(DO.Task task)
    {
        if (task.EstimatedDate == null || task.NumOfDays == null)
        {
            return null;
        }

        // המרת המספר לתאריך והוספתו לתאריך המשוער
        DateTime estimatedDateWithDays = task.EstimatedDate.Value.AddDays(task.NumOfDays.Value);

        return estimatedDateWithDays;
    }
    public void UpdateDate(int id, DateTime date)
    {
        DO.Task doTask = _dal.Task.Read(id) ??
            throw new BO.BlDoesNotExistException($"task with ID={id} dous not exist");
        BO.Task task = Read(id)!;
        List<BO.TaskInList>? Dependencies1 = task.Dependencies;

        if (Dependencies1 != null)
        {
            foreach (BO.TaskInList a in Dependencies1)//if there is  dependencies
            {
                DO.Task allTasks = _dal.Task.Read(a.TaskId) ??
                    throw new BO.BlDoesNotExistException($"task with ID={id} dous not exist");
                if (a.StartDate == null)//if the date is null
                    throw new BO.BlNotFitSchedule($"The date is null while id is:{a}");
                if (date < _dal.Schedule.GetEndPro())
                    throw new BO.BlNotFitSchedule($"task with ID={a.TaskId} will not finish in time");
            }
        }
        _dal.Task.Update(doTask with { StartDate = date });
    }

    public DateTime? startDateToSet(int id)
    {
        if (Factory.Get().Schedule.GetStage() == BO.Stage.planning)
            throw new BO.BlDoesNotExistException("");
        DO.Task tesk = _dal.Task.Read(id) ??
        throw new BO.BlDoesNotExistException("");
        IEnumerable<DO.Dependence> links = _dal.Dependence.ReadAll(Dependence => Dependence.PendingTaskId == id);
        if (links.Count() == 0)
            return _dal.Schedule.GetStartPro();
        IEnumerable<DateTime?> dates = links.Select
            (link => getPlanToFinish(_dal.Task.Read(link.PreviousTaskId)
            ?? throw new BO.BlDoesNotExistException("")));
       
        if (dates.Any(date => date == null)) return null;
        DateTime? date = dates.Max();
        return date;
    }

}
