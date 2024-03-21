
using System.Reflection;

namespace BO;

 static public class Tools
{
    public static string ToStringProporty<T>(this T obj, int indent = 0)
    {
        if (obj == null)// if the object is null
            return "Not Set";

        Type type = obj.GetType();
        if (type.IsPrimitive || type.IsValueType || type == typeof(string))// if the object is a primitive type
            return obj.ToString()!;

        string result = "";
        if (obj is System.Collections.IEnumerable enumerable)// if the object is a collrction
        {
            result += $"\n{new string(' ', indent)}[\n";
            foreach (var item in enumerable)// for each item in the collection convert it to string
                result += item.ToStringProporty(indent + 1);
            result += $"{new string(' ', indent)}]";
        }
        else// if the object is not a collection
        {
            result += $"{new string(' ', indent)}\n";
            foreach (PropertyInfo property in obj.GetType().GetProperties())
                result += new string(' ', indent + 1) + property.Name + ": " + property.GetValue(obj).ToStringProporty(indent + 1) + '\n';
            result += new string(' ', indent) + "\n";
        }
        return result;
    }
    //public static string ToStringProporty<T>(this T t)
    //   { 
    //       string str = "";
    //       IEnumerable<T> e = t as IEnumerable<T>;
    //       if (e != null)
    //       {
    //           foreach (var item in e)
    //           {
    //               str+=item.ToStringProporty();
    //           }
    //       }

    //       foreach (PropertyInfo item in t.GetType().GetProperties())
    //           str += "\n" + item.Name + ": " + item.GetValue(t, null);
    //       return (str);
    //       //    IEnumerable<T> e = t as IEnumerable<T>;
    //       //    if(e != null)
    //       //    {
    //       //        foreach(var item in e)
    //       //        {
    //       //            item.ToStringProporty();
    //       //        }
    //       //    }
    //       //    string str = "";
    //       //    foreach (PropertyInfo item in t.GetType().GetProperties())
    //       //        str += "\n" + item.Name + ": " + item.GetValue(t, null);
    //       //    return (str);
    //   }
}
