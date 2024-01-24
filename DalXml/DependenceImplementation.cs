
using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class DependenceImplementation:IDependence
{
    readonly string x_XML = "dependences";

    public int Create(Dependence item)
    {
        int newDependenceId = Config.NextDependenceId; //set newTaskId acording to the run number
        Dependence newDependence = item with { DependenceId = newDependenceId };
        var DepList = XMLTools.LoadListFromXMLSerializer<Dependence>(x_XML);
        DepList.Add(newDependence);
        XMLTools.SaveListToXMLSerializer(DepList, x_XML);
        return newDependenceId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependence? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? p)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependence item)
    {
        throw new NotImplementedException();
    }
}
