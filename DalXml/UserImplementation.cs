namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

internal class UserImplementation: IUser

{
    readonly string _xmlUser = "users";

    public void Create(User loginUser)
    {

        if (!UserExist(loginUser))
        {
            var RootUsers = XMLTools.LoadListFromXMLSerializer<User>(_xmlUser);

            RootUsers.Add(loginUser);

            XMLTools.SaveListToXMLSerializer(RootUsers, _xmlUser);
        }
        //var RootUsers = XMLTools.LoadListFromXMLSerializer<User>(_xmlUser);

        //RootUsers.Add(loginUser);
        //  XElement newItem = new XElement("User");
        // newItem.Add(new XElement("Id", loginUser.UserId));
        // newIte  m.Add(new XElement("Password", loginUser.Password));
    }

    public bool UserExist(User loginUser) =>XMLTools.LoadListFromXMLSerializer<User>(_xmlUser).Any(user => user == loginUser);

    public void DeleteAll()
    {
        XElement delItem = XMLTools.LoadListFromXMLElement(_xmlUser);
        delItem.RemoveAll();
        XMLTools.SaveListToXMLElement(delItem, _xmlUser);
    }
}
