namespace Dal;

using DalApi;
using DO;
using System.Data.Common;


internal class DependenceImplementation:IDependence
{
    readonly string x_XML_dependences = "dependences";

    public int Create(Dependence item)
    {
        int newDependenceId = Config.NextDependenceId; //set newTaskId acording to the run number
        Dependence newDependence = item with { DependenceId = newDependenceId };
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);
        DepList.Add(newDependence);
        XMLTools.SaveListToXMLSerializer(DepList, x_XML_dependences);
        return newDependenceId;
    }

    public void Delete(int id)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);

        if (DepList.Exists(x => x.DependenceId == id))
        {
            DepList.Remove(DepList.Find(x => x.DependenceId == id));
            XMLTools.SaveListToXMLSerializer(DepList, x_XML_dependences);
        }
        else
            throw new DalDoesNotExistException($"Dependence with ID={id} not exist");
    }

    public Dependence? Read(int id)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);
        foreach (Dependence dep in DepList)
        {
            if (dep.DependenceId == id) return dep;
        }
        return null;
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);
        foreach (Dependence dep in DepList)
        {
            if (filter(dep)) return dep;
        }
        return null;
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? p)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);
        if (p != null)
        {
            return from item in DepList where p(item) select item;
        }
        return from item in DepList select item;

        //updating item in the orign
    }

    public void Update(Dependence item)
    {
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML_dependences);
        Dependence? dependence = Read(item.DependenceId);
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
