
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newTaskId = DataSource.Config.NextTaskId;//set newTaskId acording to the run number
        Task newTask =item with { TaskId = newTaskId };//init a new Task
        DataSource.Tasks.Add(newTask);
        return newTaskId;
    }

    public void Delete(int id)
    {
        if (DataSource.Tasks.Exists(x => x.TaskId == id))
        {
            DataSource.Tasks.Remove(DataSource.Tasks.Find(x => x.TaskId == id));
        }
        else
            throw new DalDoesNotExistException($"Task with ID={id} not exist");
    }
    public void DeleteAll()
    {
        DataSource.Engineers.Clear();
    }
    /// <summary>
     ///  A method that take int 
     /// </summary>
     /// If a dependency with the given identifier is found, the function returns the dependency object.
     /// If no dependency with the given identifier is found, the function returns a null value.
     /// <param name="id"></param>
     /// <returns></returns>
    public Task? Read(int id) => Read(x => x.TaskId == id);
    /// <summary>
    /// A method that takes a Boolean function delegate of type Func, 
    /// operating on elements of type T in a list. 
    /// It returns the first object in the list for which the function returns True.    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// 
    public Task? Read(Func<Task, bool>? predicate=null)
    {
        return DataSource.Tasks.Where(predicate).FirstOrDefault();
    }
    /// <summary>
    /// A method that takes a Boolean function delegate of type Func, 
    /// operating on elements of type T in a list. 
    /// It returns the first object in the list for which the function returns True.    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// 
    public IEnumerable<Task> ReadAll(Func<Task, bool>? predicate = null) =>
       predicate is null ? DataSource.Tasks.Select(a => a) : DataSource.Tasks.Where(predicate);


    public void Update(Task item)
    {
        if (DataSource.Tasks.Exists(x => x.TaskId == item.TaskId))
        {
            DataSource.Tasks.Remove(DataSource.Tasks.Find(x => x.TaskId == item.TaskId));
            DataSource.Tasks.Add(item);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={item.TaskId} not exist");
    }
}
