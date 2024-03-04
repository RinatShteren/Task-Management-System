namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ISchedule = BlApi.ISchedule;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private readonly ISchedule _schedule;
    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    public TaskImplementation(ISchedule schedule) => _schedule = schedule;
 
    public int Create(BO.Task boTask)
    {

        if (_schedule.GetStage() != BO.Stage.Planning)  // Make sure the project is in the planning stage
            throw new BO.BlNotFitSchedule("Can not add tasks after Project Planning phase");

        DO.Task doTask = new DO.Task(boTask.TaskId, boTask.NickName, boTask.Description, _bl.Clock) with
        { RequiredLevel = (DO.EngineerLevel)boTask.RequiredLevel, NumOfDays = boTask.NumOfDays }; //?
        try
        {
            if (boTask.TaskId < 0)
                throw new BO.BlNotVaildException("id is not valid");

            if (string.IsNullOrEmpty(boTask.NickName))
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

        task.DeadLine = GetEndTaskDate_BO(task);
        task.Dependencies = GetLinks(task);

        return task;
    }

    public IEnumerable<TaskInList> ReadAll(Func<BO.Task, bool> p = null)
    {
        if (p == null)
        {
            IEnumerable<BO.TaskInList> tasks = (from DO.Task item in _dal.Task.ReadAll(null)
                                                select new BO.TaskInList()
                                                {
                                                    TaskId = item.TaskId,
                                                    NickName = item.NickName,
                                                    Description = item.Description,
                                                    StartDate = item.StartDate
                                                });
            return tasks;
        }
        else
        {
            IEnumerable<BO.TaskInList> tasks1 = (from DO.Task item in _dal.Task.ReadAll()
                                                 where p(doToBo(item))
                                                 select new BO.TaskInList()
                                                 {
                                                     TaskId = item.TaskId,
                                                     NickName = item.NickName,
                                                     Description = item.Description,
                                                     StartDate = item.StartDate
                                                 });
            return tasks1;
        }




    }

    public BO.Task doToBo(DO.Task doTask)
    {
        return new BO.Task()
        {
            TaskId = doTask.TaskId,
            NickName = doTask.NickName,
            Description = doTask.Description,
            RequiredLevel = (BO.EngineerLevel)doTask.RequiredLevel,
            CreationDate = (DateTime)doTask.CreationDate!,
            EstimatedDate = doTask.EstimatedDate,
            StartDate = doTask.StartDate,
            //PlanToFinish = getPlanToFinish(doTask),
            DeadLine = doTask.DeadLine,
            FinishtDate = doTask.FinishtDate,
            NumOfDays = doTask.NumOfDays,
            Product = doTask.Product,
            Remarks = doTask.Remarks,
        };
    }
   
    public void Delete(int id)
    {

        if (_schedule.GetStage() != BO.Stage.Planning)  // Make sure the project is in the planning stage
            throw new BO.BlNotFitSchedule("Can not delete tasks after Project Planning phase");


        DO.Dependence tempDp = _dal.Dependence?.Read(item => item.PreviousTaskId == id); //checking if there is another task that depended in this task
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

    public void Update(BO.Task upTask)
    {

        if (upTask.TaskId <= 0)
            throw new BO.BlNotVaildException("id is not valid");

        if (string.IsNullOrEmpty(upTask.NickName))
            throw new BO.BlNotVaildException("NickName is not valid");

        DO.Task existingTask = _dal.Task.Read(upTask.TaskId);// Read the existing task from the data layer

        if (existingTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={upTask.TaskId} does not exist");

        //אם אנחנו בתכנון אפשר לקרוא לפונקציה אפדייט של הדאל ,רק צריך לעדכן את השדה של התלויות כי הוא לא מופיע בדאל  
        if (_schedule.GetStage() != BO.Stage.Action)
        {
            // list of the tasks that the task we want to update depends on 
            List<DO.Dependence> dalDependences = _dal.Dependence.ReadAll(Dependence => Dependence.PendingTaskId == upTask.TaskId).ToList();

            //list of the tasks that the updated task depends on 
            List<DO.Dependence> upDependences = upTask.Dependencies?.Select(taskIn => new DO.Dependence(0, taskIn.TaskId, upTask.TaskId)).ToList() ?? new List<DO.Dependence>();
            // Check if task links are provided and are different from existing task links
            if (upTask.Dependencies?.Any() == true && !upDependences.SequenceEqual(dalDependences))  //אם הן שונות- חריגה 
            {
                throw new BO.BlNotFitSchedule("Dependencies cannot be initialized after creating the project schedule");
            }
            // Check if any date properties have been modified
            if (new[] { upTask.CreationDate, upTask.EstimatedDate, upTask.StartDate, upTask.DeadLine, upTask.FinishtDate }
                .Any(date => date != existingTask.CreationDate && date != existingTask.EstimatedDate &&
                             date != existingTask.StartDate && date != existingTask.DeadLine &&
                             date != existingTask.FinishtDate))
            {
                throw new BO.BlNotFitSchedule("Unable to initialize dates after creating the project schedule");
            }
        }
        //עד כאן בדקנו שלא מנסים לעשות משהו שאסור בשלב של הלוז ואם לא קפצנו אז כנראה הכל טוב ואפשר להתחיל
        try
        {
            //updete the Dependences
            foreach (var item in (_dal.Dependence.ReadAll(Dependence => Dependence.PendingTaskId == upTask.TaskId)))
            {
                _dal.Dependence.Delete(item.DependenceId);
            }
            if (upTask.Dependencies != null)
                foreach (var item in upTask.Dependencies)
                {
                    DO.Dependence temp = new DO.Dependence() { PreviousTaskId = item.TaskId, PendingTaskId = upTask.TaskId };
                    _dal.Dependence.Create(temp);
                }
            int tempID;
            if (upTask.Engineer == null)
                tempID = 0;
            else
                tempID = upTask.EngineerId;


            DO.Task doTask = new DO.Task(upTask.TaskId, upTask.Description, upTask.NickName, upTask.CreationDate, upTask.EstimatedDate, upTask.StartDate, 0, upTask.DeadLine, upTask.FinishtDate,
            upTask.Product, upTask.Remarks, tempID, (DO.EngineerLevel)upTask.RequiredLevel);

            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={upTask.TaskId} does Not exist", ex);
        }

    }

    public List<BO.TaskInList> GetLinks(BO.Task task)//return list of all dependence of spesific task
    {
        //כל התלויות שהתלות הבאה שלהם זה המשימה הנוכחית 
        List<DO.Dependence> dep = new List<DO.Dependence>(_dal.Dependence.ReadAll(link => link.PendingTaskId == task.TaskId));
        if (dep.Count == 0)//if the task not have dependence
            return new List<BO.TaskInList>();
        //else
        List<BO.TaskInList> tasks = new List<BO.TaskInList>();

        foreach (DO.Dependence d in dep)//לכל תלות 
        {

            DO.Task doTask = _dal.Task.Read(d!.PreviousTaskId) ?? throw new BO.BlDoesNotExistException("");
            //in doTask there is the task with PreviousTaskId of the current dep
            //יבדו טסק יש את המשימה שהמשימה הקודמת שלה היא 
            BO.TaskInList newTask = new BO.TaskInList//create new object of tesk in list
            //תיצור אובייקט שיכיל את השם של המשימה והתיאור והתעודת זהות
            {
                TaskId = doTask.TaskId,
                NickName = doTask.NickName,
                Description = doTask.Description
            };
            tasks.Add(newTask);//insert tasks to the taskInList 
        }
        return tasks;
    }

    public void CalculateCloserStartDateForAllTasks()
    {
        if (_schedule.GetStage() is Stage.Middle)
        {
            var tasks = _dal.Task.ReadAll(task => task.StartDate is null).ToDictionary(t => t.TaskId, t => t);

            foreach (var task in tasks.Values)//update the tasks dates
            {
                var dependenceTasks = _dal.Dependence.ReadAll(dependence => dependence.PendingTaskId == task.TaskId)
                    .Select(d => tasks[d!.PreviousTaskId]);

                var startDate = dependenceTasks switch
                {
                    var l when l.Count() is 0 => _schedule.StartProject,
                    var l when l.Any(d => d.StartDate is null) => throw new DependenceTasksStartDateIsStillNull("DependenceTasksStartDateIsStillNull"),
                    _ => GetEndTaskDate_DO(dependenceTasks.MaxBy(t =>GetEndTaskDate_DO(t!))!) 
                };
                _dal.Task.Update(task with { StartDate = startDate });
            }
        }
      
    }

    public void EnginnerToTask()
    {
        var tasks = _dal.Task.ReadAll(task => task.EngineerId >0).ToDictionary(t => t.EngineerId, t => t);
        var engineers = _dal.Engineer.ReadAll(eng => eng.Id > 0).ToList();
        var CountOfEngineers = _dal.Engineer.ReadAll(eng => eng.Id > 0).Count().ToString();
        foreach (var task in tasks.Values)
        {
            var engineerId = engineers[0];
            _dal.Task.Update(task with { EngineerId = engineerId.Id});
        }

    }

    public DateTime? GetEndTaskDate_DO(DO.Task task) => task?.StartDate!.Value.AddDays(task.NumOfDays.Value);

    public DateTime? GetEndTaskDate_BO(BO.Task task) => task?.StartDate!.Value.AddDays(task.NumOfDays.Value);

    public void UpdateDate(int id, DateTime date)//עדכון תאריך של משימה אחת
    {
        DO.Task doTask = _dal.Task.Read(id) ??
            throw new BO.BlDoesNotExistException($"task with ID={id} dous not exist");
        BO.Task task = Read(id)!;
        List<BO.TaskInList>? DepTemp = task.Dependencies;//כאן יש את התלויות של המשימה

        if (DepTemp != null)
        {
            foreach (BO.TaskInList a in DepTemp)//for each  dependencies of spesific task
            {
                DO.Task allTasks = _dal.Task.Read(a.TaskId) ??//מתוך רשימת התלויות תכניס לי משימה
                    throw new BO.BlDoesNotExistException($"task with ID={id} dous not exist");
                if (a.StartDate == null)//if the date is null
                    throw new BO.BlNotFitSchedule($"The date is null while id is:{a}");
              /*  if (date < getPlanToFinish(a))// אם התאריך שקיבלתי קטן מהתאריך של סיום המשימה  אני לא אסיים בזמן
                    throw new BO.BlNotFitSchedule($"task with ID={a.TaskId} will not finish in time");*/
            }
        }
        _dal.Task.Update(doTask with { StartDate = date });//תעדכן את המשימה שקיבלת עם התאריך
    }
}


