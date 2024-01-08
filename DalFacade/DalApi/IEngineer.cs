
namespace DalApi;
using DO;
public interface IEngineer
{
    long Create(Engineer item); //Creates new entity object in DAL
    Engineer? Read(long id); //Reads entity object by its ID
    List<Engineer?> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Engineer item); //Updates entity object
    void Delete(long id); //Deletes an object by is Id

}
