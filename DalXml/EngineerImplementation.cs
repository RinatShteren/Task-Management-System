namespace Dal;

using DalApi;
using DO;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;


internal class EngineerImplementation : IEngineer
{
    readonly string _xmlEngineer = "engineers";
    readonly string _xmlUser = "users";

    /// <summary>
    /// The method  create a new element and save it into the XML
    /// if item alredy exsist , the function throw exeption
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Engineer item)
    {
        if (Read(item.Id) != null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} is alredy exist"); //הערה שהאייטם כבר קיים בבסיס נתונים
        }
        XElement engineerRoot = XMLTools.LoadListFromXMLElement(_xmlEngineer);

        XElement newItem = new XElement("Engineer");
        newItem.Add(new XElement("Id", item.Id));
        newItem.Add(new XElement("Name", item.Name));
        newItem.Add(new XElement("Email", item.Email));
        newItem.Add(new XElement("Level", item.Level));
        newItem.Add(new XElement("Cost", item.Cost));
        engineerRoot.Add(newItem);

        XMLTools.SaveListToXMLElement(engineerRoot, _xmlEngineer);

        
        return item.Id;

    }
    /// <summary>
    /// The method  deleate an engineere from the XML
    /// if item  is empty , the function throw exeption
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement engineerRoot = XMLTools.LoadListFromXMLElement(_xmlEngineer);
        
        var delItem= engineerRoot.Elements().FirstOrDefault(item => (int?)item.Element("Id") == id);
        if (delItem.IsEmpty)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} not exist");
        }
        delItem.Remove();
        XMLTools.SaveListToXMLElement(engineerRoot, _xmlEngineer);

    }
    /// <summary>
    ///  A method that take int 
    /// </summary>
    /// If a dependency with the given identifier is found, the function returns the dependency object.
    /// If no dependency with the given identifier is found, the function returns a null value.
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    public void DeleteAll()
    {
        XElement delItem= XMLTools.LoadListFromXMLElement(_xmlEngineer);
        delItem.RemoveAll();
        XMLTools.SaveListToXMLElement(delItem, _xmlEngineer);

    }
    public Engineer? Read(int id)
    {
        XElement? engineer = XMLTools.LoadListFromXMLElement(_xmlEngineer).Elements().FirstOrDefault(item => (int?)item.Element("Id") == id);
        if (engineer != null)
            return xlmToEng(engineer);
        return null;
    }
    /// <summary>
    /// A method that takes a Boolean function delegate of type Func, 
    /// operating on elements of type T in a list. 
    /// It returns the first object in the list for which the function returns True.    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    /// 
    public Engineer? Read(Func<Engineer, bool> filter = null)
    { 
           
        return XMLTools.LoadListFromXMLElement(_xmlEngineer).Elements().Select(eng=> xlmToEng(eng)).FirstOrDefault(filter);
    }
    /// <summary>
    /// The method will receive a delegate of type Func, representing a Boolean function,
    /// operating on elements of type T in a list. It will return a list of all objects in the 
    /// list for which the function returns True. If no delegate is provided, the entire list will be returned.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? p = null)
    {

        if (p == null)//return all elements
            return XMLTools.LoadListFromXMLElement(_xmlEngineer).Elements().Select(eng => xlmToEng(eng));
        else//return all elements with condision
            return XMLTools.LoadListFromXMLElement(_xmlEngineer).Elements().Select(xlmToEng).Where(p);
            //return XMLTools.LoadListFromXMLElement(x_XML_engineers).Elements().Select(eng => xlmToEng(eng)).Where(p);

    }
    /// <summary>
    /// The method exchage an engineer item in XML file,
    /// while removing the old item of the engineer
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
 
    public void Update(Engineer item)
    {
        if (Read(item.Id) == null) //If this id doesnt exist
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");

        XElement engineerList = XMLTools.LoadListFromXMLElement(_xmlEngineer);
        Delete(item.Id);
        Create(item);
    }
    public Engineer xlmToEng (XElement eng) => new Engineer()
    {
        Id = eng.ToIntNullable("Id") ?? throw new Exception("aaaa"),
        Name = eng.Element("Name").Value,
        Email = eng.Element("Email").Value,
        Level = Enum.Parse<EngineerLevel>(eng.Element("Level").Value),
        Cost = (double)eng.Element("Cost")

    };


}
