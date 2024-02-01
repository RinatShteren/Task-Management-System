
namespace BlImplementation;
using BlApi;
using System;
using System.Collections.Generic;
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
                throw new NotImplementedException();
            if (doEngineer.Name == "")
                throw new NotImplementedException();
            if (doEngineer.Cost < 0)
                throw new NotImplementedException();
            if (!doEngineer.Email.Contains("@"))
                throw new NotImplementedException();
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new NotImplementedException();
        }
    }

    public void Delete(int id)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id);
        /*all task that fit to the current id  and to to the start date*/
        var task = _dal.Task.Read(item => item.EngineerId == id
        && item.StartDate < DateTime.Now);
        try
        {
            if(task == null)
                throw new NotImplementedException();
           
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new NotImplementedException();
        }
    }

    public BO.Engineer? Read(int id)//ask for engineer details
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new NotImplementedException();
        else
            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (BO.EngineerLevel)doEngineer.Level,
                Cost = doEngineer.Cost
            };
    }

    public IEnumerable<BO.EngineerInList> ReadAll()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(null)
                select new BO.EngineerInList
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    CurrentYear = DateTime.Now.Year //- doEngineer.RegistrationDate.Year)
                });

    }
}
