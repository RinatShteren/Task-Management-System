
namespace BlApi;

public interface IEngineer
{
    public int AddEngineer(BO.Engineer boEngineer);

    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool> p = null);
    public void Delete(int id);

    public void Update(BO.Engineer boEngineer);
    IEnumerable<BO.TaskInList> ReadAllOptionalTasksForEngineer(BO.Engineer engineer);
    void AssginTaskToEngineer(BO.Engineer engineer);
    public BO.Engineer doToBo(DO.Engineer doEng);
}
