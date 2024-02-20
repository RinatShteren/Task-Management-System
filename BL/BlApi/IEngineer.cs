
namespace BlApi;

public interface IEngineer
{
    public int AddEngineer(BO.Engineer boEngineer);

    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool> p );
    public void Delete(int id);

    public void Update(BO.Engineer boEngineer);


    public BO.Engineer doToBo(DO.Engineer doEng);
}
