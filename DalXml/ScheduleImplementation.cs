
using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class ScheduleImplementation : ISchedule
{
    private readonly string s_xml = "data-config";
    public DateTime? StartProject
    {
        get {
            XElement root = XMLTools.LoadListFromXMLElement(s_xml).Element(nameof(StartProject))!;//טוענת אלמנט מסויים
            if (root.Value == "")
                return null;
            return DateTime.Parse(root.Value);
        }
        set {
            XElement root = XMLTools.LoadListFromXMLElement(s_xml);
            root.Element(nameof(StartProject))!.Value = value.ToString()!;
            XMLTools.SaveListToXMLElement(root, s_xml);
        }
    }

    public void ResetDep()
    {

        XElement root = XMLTools.LoadListFromXMLElement(s_xml);
        int nextId1 = root.ToIntNullable("NextDependenceId") ?? throw new FormatException($"can't convert id.  {s_xml}, {"NextDependenceId"}");
        root.Element("NextDependenceId")?.SetValue((1).ToString());
        int nextId2 = root.ToIntNullable("NextTaskId") ?? throw new FormatException($"can't convert id.  {s_xml}, {"NextTaskId"}");
        root.Element("NextTaskId")?.SetValue((1).ToString());
        XMLTools.SaveListToXMLElement(root, s_xml);
        
        //XElement root = XMLTools.LoadListFromXMLElement(s_xml)!;
        //root.Element("NextDependenceId")!.Remove();
        //root.Element("NextTaskId")!.Remove();
        //XMLTools.SaveListToXMLElement(root, s_xml);
        //XElement newItem1 = new XElement("NextDependenceId");
        //newItem1.Add("1");
        //XElement newItem2 = new XElement("NextDependenceId");
        //newItem2.Add("1");
        //XMLTools.SaveListToXMLElement(root, s_xml);


        //XElement root = XMLTools.LoadListFromXMLElement(s_xml)!;

        //root.Element("NextDependenceId").SetValue("1");
        //XMLTools.SaveListToXMLElement(root, s_xml);


        //XElement root2 = XMLTools.LoadListFromXMLElement(s_xml).Element("NextTaskId")!;

        //root.SetValue("1");
        //XMLTools.SaveListToXMLElement(root2, s_xml);
    }

}
