﻿
namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation(this);

    public ISchedule Schedule => new ScheduleImplementation();
    public IUserLogin UserLogin => new UserLoginImplementation();

    public void InitalizingBD() => DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();

    
    /***   Clock Implementation  ***/
    
    private static DateTime s_Clock = DateTime.Now.Date;

    // Property for accessing the current time
    public DateTime Clock
    {
        get { return s_Clock; }
        private set { s_Clock = value; }
    }

    // Constructor to initialize the clock
    public Bl()
    {
        InitializeTime(); // Initialize the time to the current system time
    }

    // Method for advancing time by a specified number of years
    public void AdvanceTimeByYear(int years)
    {
        s_Clock = s_Clock.AddYears(years);
    }

    // Method for advancing time by a specified number of days
    public void AdvanceTimeByDay(int days)
    {
        s_Clock = s_Clock.AddDays(days);
    }

    // Method for advancing time by a specified number of hours
    public void AdvanceTimeByHour(int hours)
    {
        s_Clock = s_Clock.AddHours(hours);
    }

    // Method for initializing the time to the current system time
    public void InitializeTime()
    {
        s_Clock = DateTime.Now;
    }

}
