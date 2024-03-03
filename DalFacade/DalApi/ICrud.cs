namespace DalApi;
using DO;

public interface ICrud<T> where T : class
{
    int Create(T item); 
    T? Read(int id);
    T? Read(Func<T, bool> filter);
    IEnumerable<T> ReadAll(Func<T, bool> filter = null); 
    void Update(T item);
    void Delete(int id);
    void DeleteAll();

}
