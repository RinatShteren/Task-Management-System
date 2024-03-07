namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;



internal class TaskImplementation : ITask
{
    readonly string _xmlTask = "tasks";

    // The function accepts a task type and adds it to the appropriate XML file
    public int Create(DO.Task item)
    {
        int newTaskId = Config.NextTaskId;    //set newTaskId acording to the run number
        DO.Task newTask = item with { TaskId = newTaskId };
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);
        tskList.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tskList, _xmlTask);
        return newTaskId;
    }
    // The function accepts an id of a task type and delete the task from the appropriate XML file
    public void Delete(int id)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);

        if (tskList.Exists(x => x.TaskId == id))
        {
            tskList.Remove(tskList.Find(x => x.TaskId == id));
            XMLTools.SaveListToXMLSerializer(tskList, _xmlTask);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={id} not exist");
 
    }
    //Returns the item that corresponds to the ID or NULL if it does not exist

    public void DeleteAll()
    {
        XElement delItem = XMLTools.LoadListFromXMLElement(_xmlTask);
        delItem.RemoveAll();
        XMLTools.SaveListToXMLElement(delItem, _xmlTask);
    }
    public DO.Task? Read(int id)

    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);
        foreach (DO.Task tsk in tskList)
        {
            if (tsk.TaskId == id) return tsk;
        }
        return null;
    }
    //
    public DO.Task? Read(Func<DO.Task, bool> filter=null)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);
        foreach (DO.Task tsk in tskList)
        {
            if (filter(tsk)) return tsk;
        }
        return null;
    }
    //return the list by the filter and if no filter return yhe whole list
    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? p = null)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);
        if (p != null)
        {
            return from item in tskList where p(item) select item;
        }
        return from item in tskList select item;

        //updating item in the orign
    }
   
    public void Update(DO.Task item)
    {
        var tskList = XMLTools.LoadListFromXMLSerializer<DO.Task>(_xmlTask);
        DO.Task? task = Read(item.TaskId);
        if (task != null)
        {
            tskList.Remove(task);
            tskList.Add(item);
            XMLTools.SaveListToXMLSerializer(tskList, _xmlTask);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={item.TaskId} not exist");
    }
}
