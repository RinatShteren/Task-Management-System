
namespace DO;
/// <summary>
/// Enegeneer entity represent an engeneer with all its props
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Experience"></param>
/// <param name="Cost"></param>
public record Engineer
(
    int Id,
    string? Name = null,
    string? Email = null,
    EngineerLevel Level,
    int Cost = 0
)
{
    public Engineer() : this(0) { }
}

