using DO;
namespace DalApi;
public interface IUser
{
    bool UserExist(User loginUser);
    void Create(User loginUser);
    void DeleteAll();
}
