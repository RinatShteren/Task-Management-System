
namespace BlImplementation;
using BlApi;
using System.ComponentModel.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int AddEngineer(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
            (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerLevel)boEngineer.Level, boEngineer.Cost);
        
        try
        {
            if (doEngineer.Id < 0)
                throw new BO.BlNotVaildException("Id is not vaild");
            if (doEngineer.Name == "")
                throw new BO.BlNotVaildException("Name is empty");
            if (doEngineer.Cost < 0)
                throw new BO.BlNotVaildException("Cost under zero");
            if (!doEngineer.Email.Contains("@"))
                throw new BO.BlNotVaildException("Email not vaild");
            if ((int)doEngineer.Level<=0||(int)doEngineer.Level>4)
                throw new BO.BlNotVaildException("Level not vaild");
            
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} is alredy exist",ex);
        }
    }

    public void Delete(int id)
    {
        /*all task that fit to the current id  and to to the start date*/
        var task = _dal.Task.Read(item => item.EngineerId == id
        && item.StartDate < DateTime.Now);
        try
        {
            if(task == null)
                throw new BO.BlDeletionImpossible("You can't deleate this engineer because his task in progress");
           
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} is alredy exist", ex);
        }
    }

    public BO.Engineer? Read(int id)//ask for engineer details
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} is alredy exist");

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

    public IEnumerable<BO.Engineer> ReadAll(Func<bool>? filter = null)
    {
        if (filter == null)
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
                    engineer.Task = new BO.TaskInEngineer {Id= task.TaskId, NickName = task.NickName };
                }
            }

            return temp;
        }
        else
        {
            IEnumerable<BO.Engineer> temp = from DO.Engineer doEngineer in _dal.Engineer.ReadAll(null)
                                            where filter()
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
}
