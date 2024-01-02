
namespace DO;

public record Engineer
(
    int Id,
    string? Namel = null,
    string? Email = null,
    string? Experience = null,
    int Cost =0 
)
{
    public Engineer():this(0) { }
    //public Engineer() { }
}
