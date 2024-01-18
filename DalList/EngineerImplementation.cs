
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
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} is alredy exist"); //הערה שהאייטם כבר קיים בבסיס נתונים
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
            throw new DalDoesNotExistException($"Engineer with ID={id} not exist");
    }
    /// <summary>
    ///  A method that take int 
    /// </summary>
    /// If a dependency with the given identifier is found, the function returns the dependency object.
    /// If no dependency with the given identifier is found, the function returns a null value.
    /// <param name="id"></param>
    /// <returns></returns>
    public Engineer? Read(int id) => Read(x => x.Id == id);

    /// <summary>
    /// A method that takes a Boolean function delegate of type Func, 
    /// operating on elements of type T in a list. 
    /// It returns the first object in the list for which the function returns True.    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// 
    public Engineer? Read(Func<Engineer, bool>? predicate)
    {
        return DataSource.Engineers.Where(predicate).FirstOrDefault();
    }
    /// <summary>
    /// The method will receive a delegate of type Func, representing a Boolean function,
    /// operating on elements of type T in a list. It will return a list of all objects in the 
    /// list for which the function returns True. If no delegate is provided, the entire list will be returned.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} not exist");

    }
}
