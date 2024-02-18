
using BO;
using DalApi;

namespace BlApi;

public interface ITask
{
    public int Create(BO.Task boTask);
    public BO.Task? Read(int id);
    public IEnumerable<TaskInList> ReadAll(Func< bool>? p =null);
    public void Delete(int id);
    public void Update(BO.Task upTask);

    public void UpdateDate(int id, DateTime date);

    public List<BO.TaskInList> getLinks(BO.Task task);
   
    public void CalculateCloserStartDateForAllTasks();

    public DateTime? getEndTaskDate_DO(DO.Task task) ;

    public DateTime? getEndTaskDate_BO(BO.Task task);

    


}
