
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newTaskId = DataSource.Config.NextTaskId;//set newTaskId acording to the run number
        Task newTask = new Task(newTaskId);//init a new Task

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

  
    public Task? Read(int id) => Read(x => x.TaskId == id);
    public Task? Read(Func<Task, bool>? predicate)
    {
        return DataSource.Tasks.Where(predicate).FirstOrDefault();
    }


    public IEnumerable<Task?> ReadAll(Func<Task, bool>? predicate = null) =>
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
