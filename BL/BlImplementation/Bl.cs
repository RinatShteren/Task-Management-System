
namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation(Schedule);

    public ISchedule Schedule => new ScheduleImplementation();
    public IUserLogin UserLogin => new UserLoginImplementation();

    public void InitalizingBD() => DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();

}
