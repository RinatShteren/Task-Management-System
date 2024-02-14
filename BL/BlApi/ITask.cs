
namespace BlApi;

public interface ITask
{
    public int Create(BO.Task boTask);

    public BO.Task? Read(int id);
    public IEnumerable<BO.Task?> ReadAll();
    public void Delete(int id);

    public void UpdateDate(int id, DateTime date);
}
