
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int newDependenceId = DataSource.Config.NextDependenceId;//set newDependenceId acording to the run number
        Dependence newDependence = new Dependence(newDependenceId,0,0);//init a new Dependence

        DataSource.Dependences.Add(newDependence);
        return newDependenceId;
    }

    public void Delete(int id)
    {
        if (DataSource.Dependences.Exists(x => x.DependenceId == id))
        {
            DataSource.Dependences.Remove(DataSource.Dependences.Find(x => x.DependenceId == id));
        }
        else
            throw new Exception($"Dependence with ID={id} does Not exist");
    }

    public Dependence? Read(int id)
    {
        if (DataSource.Dependences.Exists(x => x.DependenceId == id))
            return DataSource.Dependences.Find(x => x.DependenceId == id);
        return null;
    }

    public List<Dependence?> ReadAll()
    {
        return new List<Dependence?>(DataSource.Dependences);
    }

    public void Update(Dependence item)
    {
        if (DataSource.Dependences.Exists(x => x.DependenceId == item.DependenceId))
        {
            DataSource.Dependences.Remove(DataSource.Dependences.Find(x => x.DependenceId == item.DependenceId));
            DataSource.Dependences.Add(item);
        }
        else
            throw new Exception($"Dependence with ID={item.DependenceId} does Not exist");
    }
}
