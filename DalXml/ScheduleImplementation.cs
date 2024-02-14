
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class ScheduleImplementation : ISchedule
{
    private readonly string s_xml = "data-config";

    public DateTime? GetEndPro()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_xml).Element("EndProject")!;//טוענת אלמנט מסויים
        if (root.Value == "")
            return null;
        return DateTime.Parse(root.Value);
    }

    public DateTime? GetStartPro()
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_xml).Element("StartProject")!;
        if (root.Value == "")
            return null;
        return DateTime.Parse(root.Value);
    }

    

    public DateTime? SetEndPro(DateTime endPro)//נכתיב את התאריך לdata config 
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_xml);
        root.Element("EndProject")!.Value = endPro.ToString();
        XMLTools.SaveListToXMLElement(root, s_xml);
        return endPro;
        
    }

    public DateTime? SetStartPro(DateTime startPro)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_xml);
        root.Element("StartProject")!.Value = startPro.ToString();
        XMLTools.SaveListToXMLElement(root, s_xml);//שומר לקובץ
        return startPro;

        
    }
}
