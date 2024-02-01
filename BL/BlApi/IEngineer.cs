
namespace BlApi;

public interface IEngineer
{
    public int AddEngineer(BO.Engineer boEngineer);

    public BO.Engineer? Read(int id);
    public IEnumerable <BO.EngineerInList> ReadAll();
    public void Delete(int id);
   
}
