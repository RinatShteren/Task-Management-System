
namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ISchedule Schedule { get; }
    public IUserLogin UserLogin { get; }

    public void InitalizingBD();

    public void ResetDB();


}
