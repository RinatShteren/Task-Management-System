
using System;

namespace BO;

public class TaskInList
{
    public override string ToString() => this.ToStringProporty();

    public int TaskId { get; init; }
    public string? Description { get; set; }
    public string? NickName { get; set; }//Alies

    public BO.Status Status { get; set; }

    public DateTime? StartDate { get; set; }

    // public BO.Stutus Stutus { get; set; }
}
