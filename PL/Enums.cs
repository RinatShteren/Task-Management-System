using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;


internal class EngineerLevelCollection : IEnumerable 
{
    static readonly IEnumerable<BO.EngineerLevel> s_enums =
(Enum.GetValues(typeof(BO.EngineerLevel)) as IEnumerable<BO.EngineerLevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
/*
public enum EngineerLevel
{
    Beginner,
    AdvancedBeginner,
    Intermediate,
    Advanced,
    Expert,
    All

internal class EngineerLevelCollection : IEnumerable
{
    public static readonly Dictionary<string, BO.EngineerLevel?> _engineerLevelsMap =
       Enum.GetValues(typeof(BO.EngineerLevel))
           .Cast<BO.EngineerLevel>()
           .ToDictionary(engineerLevel => engineerLevel.ToString(), engineerLevel => (BO.EngineerLevel?)engineerLevel);

    static EngineerLevelCollection()
    {
        _engineerLevelsMap.Add("All", null);
    }

    public IEnumerator GetEnumerator() =>
        _engineerLevelsMap.Values.GetEnumerator();
}}*/