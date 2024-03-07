
namespace BO;

public class Task
{
    public override string ToString() => this.ToStringProporty();

    public int TaskId { get; init; }
    public string? Description { get; set; }
    public string? NickName { get; set; }//Alies
    public EngineerLevel? RequiredLevel { get; set; }//נצרך?
    public DateTime? CreationDate { get; set; }//CreatedatDate
    public DateTime? EstimatedDate { get; set; }//מתוכנן
    public DateTime? StartDate { get; set; }//בפועל
    public int? NumOfDays { get; set; }//כמה ימים המשימה תיקח
    public DateTime? DeadLine { get; set; }//מתוכנן
    public DateTime? FinishtDate { get; set; }//בפועל
    public string? Product { get; set; }
    public string? Remarks { get; set; }//
    public int EngineerId { get; set; }//

    public int Difficulty { get; set; }
    //public EngineerLevel? RequiredLevel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public BO.EngineerInTask Engineer { get; set; }

    public List<BO.TaskInList> Dependencies { get; set; } //

}
