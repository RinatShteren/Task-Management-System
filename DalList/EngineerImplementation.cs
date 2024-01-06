
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {

        if (DataSource.Engineers.Exists(x => x.Id == item.Id))
        {
            throw new NotImplementedException(); //הערה שהאייטם כבר קיים בבסיס נתונים
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
            throw new NotImplementedException();
    }

    public Engineer? Read(int id)
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
            throw new NotImplementedException();

    }
}
