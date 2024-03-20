namespace DalApi;
using DO;


public interface IDal
{
    IEngineer Engineer { get; }
    IDependence Dependence { get; }
    IUser User { get; }
    ITask Task { get; }
    ISchedule Schedule { get; }


}
