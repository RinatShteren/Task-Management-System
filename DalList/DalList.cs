namespace Dal;
using DalApi;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependency => new DependenceImplementation();

    public ITask Task =>  new TaskImplementation();

    public ISchedule Schedule =>  new ScheduleImplementation();

    public IUser User => new UserImplementation();
}
