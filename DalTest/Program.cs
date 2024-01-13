using Dal;
using DalApi;
using DalTest;
using DO;
using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static IDependence? s_dalDependence = new DependenceImplementation(); //stage 1
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    static readonly IDal s_dal = new DalList(); //stage 2
    private static void Main(string[] args)
    {
        int num = 1;
           
        try
        {
            Initialization.Do(s_dal); //stage 2
            //Initialization.Do(s_dalEngineer, s_dalDependence, s_dalTask);
            while (num != 0)
            {
                Console.WriteLine(@"Hello!
                Enter your choice:
                0-exit
                1-test Engineer
                2-test Dependence
                3-test Task");
                string? option = Console.ReadLine();
                bool b = int.TryParse(option, out num);
                if (!b)
                {
                    Console.WriteLine("ERROR");
                    break;
                }
                switch (num)
                {
                    case 1:
                        testEngineer(s_dal.Engineer);   
                        break;
                    case 2:
                        testDependence(s_dal.Dependence);
                        break;
                    case 3:
                        testTask(s_dal.Task);
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    static void testEngineer(IEngineer? engineer)
    {
        try
        {
                Console.WriteLine(@"test order:
                Enter your choice:
                0 - EXIT MANU
                a - ADD ENGINEER
                b - GET ENGINEER BY ID
                c - GET ENGINEERS LIST
                d - UPDATE ENGINEER
                e - DELETE ENGINEER");
                string? option = Console.ReadLine();
                switch (option)
                {
                case "0":
                    break;
                case "a":
                        
                        Console.WriteLine("enter the new Engineer ID");
                        int id;
                        double cost;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("enter the Engineer name");
                        string? name = Console.ReadLine();
                        Console.WriteLine("enter the Engineer email");
                        string? email = Console.ReadLine();
                        Console.WriteLine("enter the Engineer level");
                        EngineerLevel? level = EngineerLevel.Beginner;
                        Console.WriteLine("enter the Engineer cost");
                        double.TryParse(Console.ReadLine(), out cost);

                    Engineer tempEngineer = new Engineer(id, name, email, level, cost);
                    engineer.Create(tempEngineer);
                        break;
                    case "b":
                        Console.WriteLine("enter the Engineer ID");
                        int.TryParse(Console.ReadLine(), out id);
                        int myId = id;
                        Console.WriteLine(engineer.Read(myId));
                        break;
                    case "c":
                        foreach (Engineer item in engineer.ReadAll())
                        {
                            Console.WriteLine(item);
                        }
                        /// מדפיסים את הכל
                        break;
                    case "d":
                        Engineer tempEngineer2 = new Engineer();
                        Console.WriteLine("enter the engineer ID");
                        int.TryParse(Console.ReadLine(), out id);
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("enter the Engineer name");
                        string? name2 = Console.ReadLine();
                        Console.WriteLine("enter the Engineer email");
                        string? email2 = Console.ReadLine();
                        Console.WriteLine("enter the Engineer level");
                        EngineerLevel? level2 = EngineerLevel.Beginner;/*TODO*/
                        Console.WriteLine("enter the Engineer cost");
                        double.TryParse(Console.ReadLine(), out cost);

                        engineer.Update(tempEngineer2);
                        break;
                    case "e":
                        Console.WriteLine("enter the Engineer ID");
                        int.TryParse(Console.ReadLine(), out id);
                        myId = id;
                        engineer.Delete(myId);
                        break;
                }   
        } 
       catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void testDependence(IDependence? dependence)
    {
        int DependenceId;
        int PendingTaskId;
        int PreviousTaskId;
        try
        {
            Console.WriteLine(@"test order item:
                Enter your choice:
                0 - EXIT MANU
                a - ADD Dependence
                b - GET Dependence
                c - GET Dependence LIST
                d - UPDATE Dependence
                e - DELETE Dependence");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    break;
                case "a":
                    Dependence tempDependence = new Dependence();
                    Console.WriteLine("enter the new Dependence Id");
                    int.TryParse(Console.ReadLine(), out DependenceId);
                    Console.WriteLine("enter the new Pending Task Id");
                    int.TryParse(Console.ReadLine(), out PendingTaskId);
                    Console.WriteLine("enter the new Previous Task Id");
                    int.TryParse(Console.ReadLine(), out PreviousTaskId);

                    dependence.Create(tempDependence);
                    break;
                case "b":
                    Console.WriteLine("enter the Dependence ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(dependence.Read(myId));
                    break;
                case "c":
                    foreach (Dependence oItem in dependence.ReadAll())
                    {
                        Console.WriteLine(oItem);
                    }
                    /// מדפיסים את הכל
                    break;
                case "d":
                    Dependence tempDependence2 = new Dependence();
                    Console.WriteLine("enter the new Dependence Id");
                    int.TryParse(Console.ReadLine(), out DependenceId);
                    Console.WriteLine("enter the new Pending Task Id");
                    int.TryParse(Console.ReadLine(), out PendingTaskId);
                    Console.WriteLine("enter the new Previous Task Id");
                    int.TryParse(Console.ReadLine(), out PreviousTaskId);

                    dependence.Update(tempDependence2);
                    break;
                case "e":
                    Console.WriteLine("enter the Dependence ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    dependence.Delete(myId);
                    break;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void testTask(ITask? task)
    {
        int id, numOfDays;
        int engineerId;
        bool mileStone;
        try
        {
            Console.WriteLine(@"test product:
                Enter your choice:
                0 - EXIT MANU
                a - ADD TASK
                b - GET TASK BY ID
                c - GET TASKS LIST
                d - UPDATE TASK
                e - DELETE TASK");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    break;
                case "a":

                    Console.WriteLine("enter the new task ID");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("enter the nick name");
                    string? nickName = Console.ReadLine();
                    Console.WriteLine("enter the description");
                    string? description = Console.ReadLine();
                    Console.WriteLine("enter the mile Stone");
                    bool.TryParse(Console.ReadLine(), out mileStone);
                    Console.WriteLine("enter the creation Date");
                  //  while (!DateTime.TryParse(Console.ReadLine(), out  date)) ;
                    DateTime? creationDate= null;
                    Console.WriteLine("enter the estimated Date");
                    DateTime? estimatedDate = null;
                    Console.WriteLine("enter the start Date");
                    DateTime? startDate = null;       
                    Console.WriteLine("enter the num Of Days");
                    int.TryParse(Console.ReadLine(), out numOfDays);
                    Console.WriteLine("enter the dead Line");
                    DateTime? deadLine = null;
                    Console.WriteLine("enter the finisht Date");
                    DateTime? finishtDate = null;  
                    Console.WriteLine("enter the product name");
                    string? product = Console.ReadLine();
                    Console.WriteLine("enter the remarks ");
                    string? remarks = Console.ReadLine();
                    Console.WriteLine("enter the engineer Id");
                    int.TryParse(Console.ReadLine(), out engineerId);
                    Console.WriteLine("enter the Required Level");
                    EngineerLevel? RequiredLevel = EngineerLevel.Beginner;

                    DO.Task tempTask = new DO.Task(id, nickName, description, mileStone, creationDate, estimatedDate, startDate, numOfDays, deadLine, finishtDate, product, remarks, engineerId, RequiredLevel);
                    break;
                case "b":
                    Console.WriteLine("enter the task ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);                 
                    Console.WriteLine(task.Read(myId));
                    break;
                case "c":
                    foreach (DO.Task item in task.ReadAll())
                    {
                        Console.WriteLine(item);
                    }
                        ;/// מדפיסים את הכל
                    break;
                case "d":
                
                    break;
                case "e":
                    Console.WriteLine("enter the Task ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    task.Delete(myId);
                    break;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    
}

