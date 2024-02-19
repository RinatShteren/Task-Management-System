
namespace BlApi;

public interface ISchedule
{
    public DateTime? StartProject { get; set; }

    public BO.Stage GetStage();

}
