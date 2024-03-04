
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class ScheduleImplementation : BlApi.ISchedule
{
    private readonly IDal _dal = DalApi.Factory.Get;
    private DateTime? _startProject;

    private readonly IBl _bl;
    internal ScheduleImplementation(IBl bl) => _bl = bl;
    public DateTime? StartProject
    {
        get { return _dal.Schedule.StartProject; }
        set { _dal.Schedule.StartProject = value;}
    }

    public BO.Stage GetStage()
    {
        if(StartProject is null)
            return BO.Stage.Planning;

        if(!_dal.Task.ReadAll(t => t.StartDate is not null).Any())
            return BO.Stage.Middle;

        return BO.Stage.Action;
    }
}
