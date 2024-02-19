
using DalApi;
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
}
