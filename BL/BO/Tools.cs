
using System.Reflection;

namespace BO;

 static public class Tools
{
 public static string ToStringProporty<T>(this T t)
    {
        IEnumerable<T> e = t as IEnumerable<T>;
        if(e != null)
        {
            foreach(var item in e)
            {
                item.ToStringProporty();
            }
        }
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
            str += "\n" + item.Name + ": " + item.GetValue(t, null);
        return (str);
    }
}
