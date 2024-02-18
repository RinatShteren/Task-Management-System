
namespace BO;

public class EngineerInTask
{
    public override string ToString() => this.ToStringProporty();

    public int Id { get; init; }
    public string? Name { get; set; }
   // public bool HasTask { get; set; }
}
