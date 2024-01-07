
namespace DalTest;
using DalApi;
using DO;
using System.Reflection.Emit;

public static class Initialization
{
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependence? s_dalDependence; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();


    private static void createEngineer()
    {
        string[] engineerNames =
        {
        "Dani Levi", "Eli Amar", "Neria Cohen",
        "Ariela Levin", "Dina Klein"
        };
        
       
        foreach (var _name in engineerNames)
        {
            int _id;

            do
                _id = s_rand.Next(200000000, 400000000);
            while (s_dalEngineer!.Read(_id) != null);

            string? _email = _name[0]+ _name[1] + "@gmail.com";

            EngineerLevel? _engineerLevel = EngineerLevel.Beginner;//DOTO
            double _cost = s_rand.Next(200, 1000);
              Engineer newStu = new(_id, _name, _email, _engineerLevel, _cost);

            s_dalEngineer!.Create(newStu);
        }
    }


}
