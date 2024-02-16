
using BO;
using DalApi;

namespace BlImplementation;

internal class ScheduleImplementation : BlApi.ISchedule
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime? GetEndPro() => _dal.Schedule.GetEndPro();

    public DateTime? GetStartPro() => _dal.Schedule.GetStartPro();

    public DateTime? SetEndPro(DateTime endPro) => _dal.Schedule.SetEndPro(endPro);

    public DateTime? SetStartPro(DateTime startPro) => _dal.Schedule.SetStartPro(startPro);

    public BO.Stage GetStage()
    {
        if(GetStartPro() is null)
            return BO.Stage.Planning;
        if(!_dal.Task.ReadAll(t => t.StartDate is not null).Any())
            return BO.Stage.Middle;
        return BO.Stage.Action;
    }
}
