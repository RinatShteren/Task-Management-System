
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependenceImplementation : IDependence
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
            throw new DalDoesNotExistException($"Dependence with ID={id} does Not exist");
    }
    /// <summary>
    ///  A method that take int 
    /// </summary>
    /// If a dependency with the given identifier is found, the function returns the dependency object.
    /// If no dependency with the given identifier is found, the function returns a null value.
    /// <param name="id"></param>
    /// <returns></returns>
    public Dependence? Read(int id) => Read(x => x.DependenceId == id);
    /// <summary>
    /// A method that takes a Boolean function delegate of type Func, 
    /// operating on elements of type T in a list. 
    /// It returns the first object in the list for which the function returns True.    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Dependence? Read(Func<Dependence, bool>? predicate)
    {
        return DataSource.Dependences.Where(predicate).FirstOrDefault();
    }
    /// <summary>
    /// The method will receive a delegate of type Func, representing a Boolean function,
    /// operating on elements of type T in a list. It will return a list of all objects in the 
    /// list for which the function returns True. If no delegate is provided, the entire list will be returned.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? predicate = null) =>
        predicate is null ? DataSource.Dependences.Select(a => a) : DataSource.Dependences.Where(predicate);

    /// <summary>
    /// Updating Data of an Existing Object
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Dependence item)
    {
        if (DataSource.Dependences.Exists(x => x.DependenceId == item.DependenceId))
        {
            DataSource.Dependences.Remove(DataSource.Dependences.Find(x => x.DependenceId == item.DependenceId));
            DataSource.Dependences.Add(item);
        }
        else
            throw new DalDoesNotExistException($"Dependence with ID={item.DependenceId} does Not exist");
    }
}
