
namespace DO;
/// <summary>
/// Specifies which task can only be performed after the completion of a previous task
/// </summary>
/// <param name="DependenceId">Unique ID number of the Dependence</param>
/// <param name="PendingTaskId">ID number of pending task</param>
/// <param name="PreviousTaskId">Previous assignment ID number</param>
public record Dependence
(
    int DependenceId,
    int PendingTaskId,
    int PreviousTaskId
)
{
    public Dependence() : this(0,0,0) { }
}
