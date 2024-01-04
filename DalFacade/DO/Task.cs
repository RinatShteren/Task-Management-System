
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// Task Entity represents a Task with all its props
/// </summary>
/// <param name="TaskId">Unique ID number of the task</param>
/// <param name="NickName"></param>
/// <param name="Description">A text detailing the task</param>
/// <param name="MileStone"></param>
/// <param name="CreationDate">The time when the task was created by the manager</param>
/// <param name="EstimatedDate">Planned date for work to begin</param>
/// <param name="StartDate">Date of actual start of work on the assignment</param>
/// <param name="NumOfDays">The number of working days required to complete the task</param>
/// <param name="DeadLine">The latest possible date on which the completion of the task will not cause the project to fail</param>
/// <param name="FinishtDate">Actual work completion date</param>
/// <param name="Product">The results or items provided at the end of the task</param>
/// <param name="Remarks"></param>
public record Task
(
    int TaskId,                             
    string? NickName = null,            
    string? Description = null,         
    bool MileStone = false,              
    DateTime? CreationDate= null,         
    DateTime? EstimatedDate = null,        
    DateTime? StartDate = null,         
    int? NumOfDays=null,                
    DateTime? DeadLine = null,          
    DateTime? FinishtDate = null,       
    string? Product=null,               
    string? Remarks = null,
    int EngineerId=0,
    EngineerLevel? RequiredLevel = null

)
{
    public Task() : this(0) { }
}
