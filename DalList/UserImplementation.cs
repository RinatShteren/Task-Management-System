using DalApi;
using DO;

namespace Dal;

internal class UserImplementation : IUser
{
    public void Create(User loginUser)
    {
        if (!UserExist(loginUser)) DataSource.Users.Add(loginUser);
    }

    public bool UserExist(User loginUser) => DataSource.Users.Any(user => loginUser == user);

    public void DeleteAll()
    {
        DataSource.Users.Clear();
    }


}