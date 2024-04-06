namespace Dal;

using DalApi;
using DO;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;



internal class DependenceImplementation:IDependence
{
    readonly string x_XML_dependences = "dependences";

    public int Create(Dependency item)
    {
        int newDependenceId = Config.NextDependenceId; //set newTaskId acording to the run number
        Dependency newDependence = item with { DependenceId = newDependenceId };
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);
        DepList.Add(newDependence);
        XMLTools.SaveListToXMLSerializer(DepList, x_XML_dependences);
        return newDependenceId;
    }

    public void Delete(int id)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);

        if (DepList.Exists(x => x.DependenceId == id))
        {
            DepList.Remove(DepList.Find(x => x.DependenceId == id));
            XMLTools.SaveListToXMLSerializer(DepList, x_XML_dependences);
        }
        else
            throw new DalDoesNotExistException($"Dependence with ID={id} not exist");
    }
    public void DeleteAll()
    {
        XElement delItem = XMLTools.LoadListFromXMLElement(x_XML_dependences);
        delItem.RemoveAll();
        XMLTools.SaveListToXMLElement(delItem, x_XML_dependences);
    }
    public Dependency? Read(int id)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);
        foreach (Dependency dep in DepList)
        {
            if (dep.DependenceId == id) return dep;
        }
        return null;
    }

    public Dependency? Read(Func<Dependency, bool> filter= null)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);
        foreach (Dependency dep in DepList)
        {
            if (filter(dep)) return dep;
        }
        return null;
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? p= null)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);
        if (p != null)
        {
            return from item in DepList where p(item) select item;
        }
        return from item in DepList select item;

        //updating item in the orign
    }

    public void Update(Dependency item)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependency>(x_XML_dependences);
        Dependency? dependence = Read(item.DependenceId);
        if (dependence != null)
        {
            DepList.Remove(dependence);
            DepList.Add(item);
            XMLTools.SaveListToXMLSerializer(DepList, x_XML_dependences);
        }
        else
            throw new DalDoesNotExistException($"Dependence with ID={item.DependenceId} not exist");
    }
}
