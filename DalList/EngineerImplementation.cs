
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {

        if (DataSource.Engineers.Exists(x => x.Id == item.Id))
        {
            throw new Exception($"Engineer with ID={item.Id} is alredy exist"); //הערה שהאייטם כבר קיים בבסיס נתונים
        }

        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        if (DataSource.Engineers.Exists(x => x.Id == id))
        {
            DataSource.Engineers.Remove(DataSource.Engineers.Find(x => x.Id == id));
        }
        else
            throw new Exception($"Engineer with ID={id} not exist");
    }

    public Engineer? Read(int id) => Read(x => x.Id == id);
    public Engineer? Read(Func<Engineer, bool>? predicate)
    {
        return DataSource.Engineers.Where(predicate).FirstOrDefault();
    }


    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? predicate = null) =>
       predicate is null ? DataSource.Engineers.Select(a => a) : DataSource.Engineers.Where(predicate);

    
    public void Update(Engineer item)
    {
        if (DataSource.Engineers.Exists(x => x.Id == item.Id))
        {
            DataSource.Engineers.Remove(DataSource.Engineers.Find(x => x.Id == item.Id));
            DataSource.Engineers.Add(item);
        }
        else
            throw new Exception($"Engineer with ID={item.Id} not exist");

    }
}
