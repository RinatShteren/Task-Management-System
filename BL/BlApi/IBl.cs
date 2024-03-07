
namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ISchedule Schedule { get; }
    public IUserLogin UserLogin { get; }

    public DateTime Clock { get; }

    public void InitalizingBD();

    public void ResetDB();

    public void AdvanceTimeByYear(int years);

    public void AdvanceTimeByDay(int days);

    public void AdvanceTimeByMonth(int month);

    public void InitializeTime();


}
