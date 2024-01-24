using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class TaskImplementation : ITask
{
    readonly string x_XML_tasks = "tasks";

    // The function accepts a task type and adds it to the appropriate XML file
    public int Create(DO.Task item)
    {
        int newTaskId = Config.NextTaskId;    //set newTaskId acording to the run number
        DO.Task newTask = item with { TaskId = newTaskId };
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);
        tskList.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tskList, x_XML_tasks);
        return newTaskId;
    }
    // The function accepts an id of a task type and delete the task from the appropriate XML file
    public void Delete(int id)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);

        if (tskList.Exists(x => x.TaskId == id))
        {
            tskList.Remove(tskList.Find(x => x.TaskId == id));
            XMLTools.SaveListToXMLSerializer(tskList, x_XML_tasks);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={id} not exist");
 
    }
    //Returns the item that corresponds to the ID or NULL if it does not exist
    public DO.Task? Read(int id)

    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);
        foreach (DO.Task tsk in tskList)
        {
            if (tsk.TaskId == id) return tsk;
        }
        return null;
    }
    //
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);
        foreach (DO.Task tsk in tskList)
        {
            if (filter(tsk)) return tsk;
        }
        return null;
    }
    //return the list by the filter and if no filter return yhe whole list
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? p)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);
        if (p != null)
        {
            return from item in tskList where p(item) select item;
        }
        return from item in tskList select item; 
    
        //updating item in the orign
    public void Update(DO.Task item)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(x_XML_tasks);
        DO.Task? task = Read(item.TaskId);
        if (task != null)
        {
            tskList.Remove(task);
            tskList.Add(item);
            XMLTools.SaveListToXMLSerializer(tskList, x_XML_tasks);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={item.TaskId} not exist");
    }
}
