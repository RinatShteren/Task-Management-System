
namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private readonly BlApi.ITask _task;

    private readonly IBl _bl;
    internal EngineerImplementation(IBl bl) => _bl = bl;


    public EngineerImplementation(BlApi.ITask task) => _task = task;

    public int AddEngineer(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
            (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerLevel)boEngineer.Level, boEngineer.Cost);

        try
        {
            if (double.IsNegative(doEngineer.Id)) throw new BO.BlNotVaildException("Id is not vaild");

            if (string.IsNullOrWhiteSpace(doEngineer.Name)) throw new BO.BlNotVaildException("Name is empty");

            if (double.IsNegative(doEngineer.Cost)) throw new BO.BlNotVaildException("Cost under zero");

            if (!new EmailAddressAttribute().IsValid(doEngineer.Email)) throw new BO.BlNotVaildException("Email not vaild");

            if ((int)doEngineer.Level! > Enum.GetValues(typeof(DO.EngineerLevel)).Length) throw new BO.BlNotVaildException("Level not vaild");

            int idEng = _dal.Engineer.Create(doEngineer);
            //Create a User
            _dal.User.Create(new DO.User(new Random().Next(2000, 4000), boEngineer.Id));

            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} is alredy exist", ex);
        }
    }

    public BO.Engineer? Read(int id)//ask for engineer details
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} is not valid");

        BO.Engineer engeneerToRead = new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerLevel)doEngineer.Level,
            Cost = doEngineer.Cost
        };

        var task = _dal.Task.Read(item => item.EngineerId == id);
        //if we found an engineer
        if (task != null)
        {
            BO.TaskInEngineer a = new BO.TaskInEngineer() { Id = task.TaskId, NickName = task.NickName };
            engeneerToRead.Task = a;

        }
        return engeneerToRead;
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool> p = null)
    {


        if (p == null)
        {
            IEnumerable<BO.Engineer> temp = from DO.Engineer doEngineer in _dal.Engineer.ReadAll(null)
                                            select new BO.Engineer()
                                            {
                                                Id = doEngineer.Id,
                                                Name = doEngineer.Name,
                                                Email = doEngineer.Email,
                                                Level = (BO.EngineerLevel)doEngineer.Level,
                                                Cost = doEngineer.Cost,
                                            };

            foreach (BO.Engineer engineer in temp)
            {
                DO.Task? task = _dal.Task.Read(item => item.EngineerId == engineer.Id); //searching for the task that the engineer is responsible for

                if (task != null)
                {
                    engineer.Task = new BO.TaskInEngineer { Id = task.TaskId, NickName = task.NickName };
                }
            }

            return temp;
        }
        else
        {
            IEnumerable<BO.Engineer> temp = from DO.Engineer doEngineer in _dal.Engineer.ReadAll(null)
                                            where p(doToBo(doEngineer))
                                            select new BO.Engineer()
                                            {
                                                Id = doEngineer.Id,
                                                Name = doEngineer.Name,
                                                Email = doEngineer.Email,
                                                Level = (BO.EngineerLevel)doEngineer.Level,
                                                Cost = doEngineer.Cost,
                                            };

            foreach (BO.Engineer engineer in temp)
            {
                DO.Task? task = _dal.Task.Read(item => item.EngineerId == engineer.Id); //searching for the task that the engineer is responsible for

                if (task != null)
                {
                    engineer.Task = new BO.TaskInEngineer { Id = task.TaskId, NickName = task.NickName };
                }
            }

            return temp;
        }
    }

    public void Delete(int id)
    {
        /*all task that fit to the current id  and to to the start date*/
        var task = _dal.Task.Read(item => item.EngineerId == id
        && item.StartDate < _bl.Clock);
        try
        {
            if (task == null)
                throw new BO.BlDeletionImpossible("You can't deleate this engineer because his task in progress");

            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} is alredy exist", ex);
        }
    }

    public void Update(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
            (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerLevel)boEngineer.Level, boEngineer.Cost);
        try
        {
            if (double.IsNegative(doEngineer.Id)) throw new BO.BlNotVaildException("Id is not vaild");

            if (string.IsNullOrWhiteSpace(doEngineer.Name)) throw new BO.BlNotVaildException("Name is empty");

            if (double.IsNegative(doEngineer.Cost)) throw new BO.BlNotVaildException("Cost under zero");

            if (!new EmailAddressAttribute().IsValid(doEngineer.Email)) throw new BO.BlNotVaildException("Email not vaild");

            if ((int)doEngineer.Level! > Enum.GetValues(typeof(DO.EngineerLevel)).Length) throw new BO.BlNotVaildException("Level not vaild");

            //UPDATE the engineer if content is vaild

            _dal.Engineer.Update(doEngineer);

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} dous not exist", ex);
        }

    }
    public IEnumerable<BO.TaskInList> ReadAllOptionalTasksForEngineer(BO.Engineer engineer)
    {
        return _bl.Task.ReadAllOptionalTasksForEngineer(engineer);

       // return _task.ReadAllOptionalTasksForEngineer(engineer);
        }

    public void AssginTaskToEngineer(BO.Engineer engineer)
    {
        //if he has tasts 
        //if (engineer.Task != null)
        //var TempEngineer = _dal.Engineer.Read(eng => (eng == engineer.Task!.Id))//&& task.EngineerId == engineer.Id
        // ?? throw new BlDoesNotExistException("task dous not fit");

        //_dal.Engineer.Update(TempEngineer with { Task = engineer.Id });
        _bl.Task.AssginTaskToEngineer(engineer);
    }

    public BO.Engineer doToBo(DO.Engineer doEng)
    {
        BO.Engineer boEng = new BO.Engineer()
        {
            Id = doEng.Id,
            Name = doEng.Name,
            Email = doEng.Email,
            Level = (BO.EngineerLevel)doEng.Level,
            Cost = doEng.Cost
        };
        //check if there is a task on track of the engineer
        var task = _dal.Task.Read(item => item.TaskId == doEng.Id);

        if (task != null) //if found 
        {
            BO.TaskInEngineer temp = new BO.TaskInEngineer() { Id = task.TaskId, NickName = task.NickName };
            boEng.Task = temp;
        }
        return boEng;
    }
}