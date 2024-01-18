
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    readonly string x_XML_engineers = "engineers";

    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} is alredy exist"); //הערה שהאייטם כבר קיים בבסיס נתונים
        }
        XElement engineerRoot = XMLTools.LoadListFromXMLElement(x_XML_engineers);

        XElement newItem = new XElement("Engineer");
        newItem.Add(new XElement("Id", item.Id));
        newItem.Add(new XElement("Name", item.Name));
        newItem.Add(new XElement("Email", item.Email));
        newItem.Add(new XElement("Level", item.Level));
        newItem.Add(new XElement("Cost", item.Cost));
        engineerRoot.Add(newItem);

        XMLTools.SaveListToXMLElement(engineerRoot, x_XML_engineers);
        return item.Id;

    }

    public void Delete(int id)
    {
        XElement engineerRoot = XMLTools.LoadListFromXMLElement(x_XML_engineers);
        XElement delItem;

        delItem = (from i in engineerRoot.Elements()
                   where Convert.ToInt32(i.Element("Id").Value) == id
                   select i).FirstOrDefault();
        if (delItem.IsEmpty)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exist");

        }
        delItem.Remove();
        XMLTools.SaveListToXMLElement(engineerRoot, x_XML_engineers);

    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? p)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
