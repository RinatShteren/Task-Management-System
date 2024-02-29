using BlApi;
using DO;

namespace BlImplementation;

internal class UserLoginImplementation: IUserLogin
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public bool UserExist(BO.User loginUser)
    {
        bool isUserExist = _dal.User.UserExist(new User(loginUser.Password, loginUser.UserId));

        return loginUser.UserId is null ? isUserExist : _dal.Engineer.Read(loginUser.UserId.Value) is not null;
    }
}