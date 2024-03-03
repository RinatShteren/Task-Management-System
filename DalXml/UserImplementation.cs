
using DalApi;
using DO;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;




namespace Dal;

internal class UserImplementation: IUser

{
    readonly string _xmlUser = "users";

    public void Create(User loginUser)
    {
        if (!UserExist(loginUser))
        {
            var RootUsers = XMLTools.LoadListFromXMLSerializer<User>(_xmlUser);
            // users.Add(loginUser);
            XElement newItem = new XElement("User");

            newItem.Add(new XElement("Id", loginUser.UserId));
            newItem.Add(new XElement("Password", loginUser.Password));
            XMLTools.SaveListToXMLSerializer(RootUsers, _xmlUser);

            //RootUsers.Add(loginUser);

          

        }
    }

    public bool UserExist(User loginUser) => XMLTools.LoadListFromXMLSerializer<User>(_xmlUser).Any(user => user == loginUser);
}
