
using DalApi;
using DO;

namespace Dal;

internal class DependenceImplementation:IDependence
{
    readonly string x_XML = "dependences";

    public int Create(Dependence item)
    {
        throw new NotImplementedException();
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
