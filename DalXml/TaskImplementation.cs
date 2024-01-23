using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class TaskImplementation : ITask
{
    readonly string x_XML = "tasks";

    public int Create(DO.Task item)
    {
        int newTaskId = Config.NextTaskId; //set newTaskId acording to the run number
        DO.Task newTask = item with { TaskId = newTaskId };
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML);
        tskList.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tskList, x_XML);
        return newTaskId;
    }

    public void Delete(int id)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML);
        
        if (tskList.Exists(x => x.TaskId == id))
        {
            tskList.Remove(tskList.Find(x => x.TaskId == id));
        }
        else
            throw new DalDoesNotExistException($"Task with ID={id} not exist");
        XMLTools.SaveListToXMLSerializer(tskList, x_XML);
    }

    public DO.Task? Read(int id)

    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML);
        foreach(DO.Task tsk in tskList) {
        if(tsk.TaskId == id) return tsk;
        }
        return null;    
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? p)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
