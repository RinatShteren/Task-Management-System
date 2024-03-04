
namespace BlApi;

internal interface IGantt
{
   // public BO.Gantt? Read(int id);
    public IEnumerable<BO.Gantt> ReadAll(Func<BO.Gantt, bool> p = null);
}
