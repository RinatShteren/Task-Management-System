
namespace BO;

public class Task
{
    public int TaskId { get; init; }
    public string? Description { get; set; }
    public string? NickName { get; set; }//Alies
    public DateTime? CreationDate { get; set; }//CreatedatDate
    public DateTime? EstimatedDate { get; set; }
    public DateTime? StartDate { get; set; }
    public int? NumOfDays { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime? FinishtDate { get; set; }
    public string? Product { get; set; }
    public string? Remarks { get; set; }//
    public int EngineerId { get; set; }//
    public EngineerLevel? RequiredLevel { get; set; }
 
    /// <summary>
    /// 
    /// </summary>
    public BO.EngineerInTask Engineer { get; set; }

    public List<BO.TaskInList>? Dependencies { get; set; } //

}
