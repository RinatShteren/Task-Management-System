
namespace BlApi;

public interface IEngineer
{
    public int AddEngineer(BO.Engineer boEngineer);

    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<bool> p );
    public void Delete(int id);

    public void Update(BO.Engineer boEngineer);



}
