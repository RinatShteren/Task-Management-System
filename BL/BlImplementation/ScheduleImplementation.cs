
using BO;
using DalApi;

namespace BlImplementation;

internal class ScheduleImplementation : ISchedule
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime? GetEndPro() => _dal.Schedule.GetEndPro();

    public DateTime? GetStartPro() => _dal.Schedule.GetStartPro();

    public DateTime? SetEndPro(DateTime endPro) => _dal.Schedule.SetEndPro(endPro);

    public DateTime? SetStartPro(DateTime startPro) => _dal.Schedule.SetStartPro(startPro);

    public BO.Stage GetStage()
    {
        if(this.GetStartPro().Value == null)
            return BO.Stage.planning;
        else if(this.GetStartPro().Value != null/*וגם כל המשימות בדאל הם עם תאריך התחלה נל*/)
            return BO.Stage.middle;
        else
            return BO.Stage.action;
    }

   
   

}
