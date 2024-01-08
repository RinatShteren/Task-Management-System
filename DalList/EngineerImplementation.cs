
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
public class EngineerImplementation : IEngineer
{
    public long Create(Engineer item)
    {

        if (DataSource.Engineers.Exists(x => x.Id == item.Id))
        {
            throw new Exception($"Engineer with ID={item.Id} is alredy exist"); //הערה שהאייטם כבר קיים בבסיס נתונים
        }

        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(long id)
    {
        if (DataSource.Engineers.Exists(x => x.Id == id))
        {
            DataSource.Engineers.Remove(DataSource.Engineers.Find(x => x.Id == id));
        }
        else
            throw new Exception($"Engineer with ID={id} not exist");
    }

    public Engineer? Read(long id)
    {
        if (DataSource.Engineers.Exists(x => x.Id == id))
            return DataSource.Engineers.Find(x => x.Id == id);
        return null;
    }

    public List<Engineer?> ReadAll()
    {
        return new List<Engineer?>(DataSource.Engineers);
    }

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
