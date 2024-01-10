namespace Dal;
using DalApi;

sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public ITask Task =>  new TaskImplementation();

}
