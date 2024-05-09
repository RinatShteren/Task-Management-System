
namespace BO;

public class Gantt
{
    public int TaskId { get; init; }
    public required string TaskName { get; init; }
    public  int EngineerId { get; init; }
    public required string EngineerName { get; init; }
    public  string? Description { get; init; }
    public DateTime? StartDate { get; init; }

}
