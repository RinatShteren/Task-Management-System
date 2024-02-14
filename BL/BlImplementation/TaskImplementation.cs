namespace BlImplementation;
using BlApi;
//using BO;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task boTask)
    {
        // ודא שהפרויקט לא בשלב התכנון
        if (Factory.Get.Stage != BO.Stage.Planning) 
            throw new BO.BlNotFitSchedule("Can not add tasks after Project planning phase");

        DO.Task doTask = new DO.Task(boTask.TaskId, boTask.Description, boTask.NickName); //?
  
        try
        {
            if (boTask.TaskId < 0) //??
                throw new BO.BlNotVaildException("id is not valid");

            if (boTask.NickName == "")
                throw new BO.BlNotVaildException("NickName is not valid");

            int idTsk = _dal.Task.Create(doTask); // צור את המשימה בשכבת הנתונים

            if (boTask.Dependencies != null) //if the task depend in ather task ??
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
       //בדיקה אם יש משימה שתלויה במשימה שעומדים למחוק
        DO.Dependence tempDp= _dal.Dependence.Read(item => item.PreviousTaskId == id);
        if (tempDp != null)
            throw new NotImplementedException();
        try
        {
            DO.Task tempTsk = _dal.Task.Read(id);
            if (tempTsk == null) throw new NotImplementedException();
            if (tempTsk.EstimatedDate != null) throw new NotImplementedException(); //אסור למחוק אחרי שנוצר לוח זמנים לפרוייקט
            else _dal.Task.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new NotImplementedException();
        }
    }
  


    public IEnumerable<Task?> ReadAll()
    {
        throw new NotImplementedException();
    }
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");

        BO.Task task = new BO.Task()
        {
            TaskId = id,
            Description = doTask.Description,
            NickName = doTask.NickName,
            MileStone = doTask.MileStone,
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
                Name = (_dal.Engineer.Read(id) ?? throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist")).Id = (int)doTask.Name,

            };
        }

        task.Status = getStatus(doTask);
        task.PlanToFinish = getPlanToFinish(doTask);
        task.Links = getLinks(task);

        return task;
    }

}
