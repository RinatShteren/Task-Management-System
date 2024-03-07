
namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ISchedule Schedule { get; }
    public IUserLogin UserLogin { get; }

    public void InitalizingBD();

    public void ResetDB();

    public DateTime Clock { get; } // Property for accessing current date and time

    // Methods for advancing time units
    public void AdvanceTimeByYear(int years);
    public void AdvanceTimeByDay(int days);
    public void AdvanceTimeByHour(int hours);

    // Method for initializing time
    public void InitializeTime();
    void AdvanceTimeByMonth(int v);
}
