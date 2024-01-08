
namespace DO;
/// <summary>
/// Enegeneer entity represent an engeneer with all its props
/// </summary>
/// <param name="Id"> Unique ID number </param>
/// <param name="Name">The name of the engineer</param>
/// <param name="Email">The Email of the engineer</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">cost per hour</param>
public record Engineer
(
    long Id,
    string? Name = null,
    string? Email = null,
    EngineerLevel? Level = null,
    double Cost = 0 
)
{
    public Engineer() : this(0) { }
}

