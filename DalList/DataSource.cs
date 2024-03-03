
namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startDependenceId = 1000;
        private static int nextDependenceId = startDependenceId;
        internal static int NextDependenceId { get => nextDependenceId++; }

        internal const int startTaskId = 1000;
        
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

    }
    //________________________
    internal static DateTime? StartDate = null;
    internal static DateTime? EndDate = null;

    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Dependence> Dependences { get; } = new();

    internal static List<DO.User> Users { get; } = new();
}
