
using DalApi;

namespace Dal;

internal class ScheduleImplementation : ISchedule
{
    private readonly string s_xml = "data-config";

	private DateTime? _startProject;

	public DateTime? StartProject
    {
		get { return  _startProject; }
		set {  _startProject = value; }
	}
}
