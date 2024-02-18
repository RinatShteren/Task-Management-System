
namespace BO;

public class EngineerInList
{
    public override string ToString() => this.ToStringProporty();

    public int Id { get; init; }
    public string? Name { get; set; }
    public int CurrentYear { get; set; } 

}
