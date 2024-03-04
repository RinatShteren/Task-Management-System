

namespace BlApi;
using BO;
using DalApi;

public interface ITask
{
    public int Create(BO.Task boTask);
    public BO.Task? Read(int id);

    public IEnumerable<TaskInList> ReadAll(Func<BO.Task, bool> p = null);
    public void Delete(int id);
    public void Update(BO.Task upTask);

    public List<BO.TaskInList> GetLinks(BO.Task task);
   
    public void CalculateCloserStartDateForAllTasks();

    public DateTime? GetEndTaskDate_DO(DO.Task task);

    public DateTime? GetEndTaskDate_BO(BO.Task task);

    public void UpdateDate(int id, DateTime date);

    public BO.Task doToBo(DO.Task doTask);

    public void EnginnerToTask();
}
