namespace BO;

public class Engineer
{
    public override string ToString() => this.ToStringProporty();
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerLevel? Level { get; set; } 
    public double Cost { get; set; }
    public BO.TaskInEngineer Task { get; set; }
}
