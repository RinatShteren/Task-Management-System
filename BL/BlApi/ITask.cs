
using BO;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task boTask);

    public BO.Task? Read(int id);
    public IEnumerable<TaskInList> ReadAll(Func<BO.Task, bool>? p);
    public void Delete(int id);
    public void UpdateDate(int id, DateTime date);
    public List<BO.TaskInList> getLinks(BO.Task task);
   public DateTime? getPlanToFinish(DO.Task task);


}
