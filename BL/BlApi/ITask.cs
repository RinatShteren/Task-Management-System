
using BO;
using DalApi;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task boTask);
    public BO.Task? Read(int id);
    public IEnumerable<TaskInList> ReadAll(Func<BO.Task, bool>? p =null);
    public void Delete(int id);
    public void Update(BO.Task upTask);

    public void UpdateDate(int id, DateTime date);
    public void UpdateEstimatedDate(int id, DateTime date);
    public void UpdateDeadLineDate(int id, DateTime date);

    public List<BO.TaskInList> getLinks(BO.Task task);
    public DateTime? getPlanToFinish(int id);
    void CalculateCloserStartDateForAllTasks();

  
}
