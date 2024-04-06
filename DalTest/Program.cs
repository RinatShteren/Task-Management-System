namespace DalTest;
using Dal;
using DalApi;
using DO;
using System;
using System.Data.SqlTypes;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4
    private static void Main(string[] args)
    {
        int num = 1;

        try
        {
            // Stage 2: Perform initialization
            //Initialization.Do(s_dal);

            // Main loop for user inter
            // 
            while (num != 0)
            {
                Console.WriteLine(@"Hello!
                Enter your choice:
                0 - exit
                1 - test Engineer
                2 - test Dependence
                3 - test Task
                4 - Generation initial data ");

                // Read user input
                string? option = Console.ReadLine();

                // Check if the input is a valid integer
                bool isNumeric = int.TryParse(option, out num);

                // Handle non-numeric input
                if (!isNumeric)
                {
                    Console.WriteLine("ERROR: Invalid input. Please enter a number.");
                    break;
                }

                // Execute corresponding test based on user choice
                switch (num)
                {
                    case 1:
                        // Test Engineer functionality
                        testEngineer(s_dal.Engineer);
                        break;
                    case 2:
                        // Test Dependence functionality
                        testDependence(s_dal.Dependency);
                        break;
                    case 3:
                        // Test Task functionality
                        testTask(s_dal.Task);
                        break;
                    case 4:
                        // Stage 2: Perform initialization
                        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                        if (ans == "Y") //stage 3
                            Initialization.Do(); //stage 4
                        break;
                    default:
                        // Handle unrecognized input
                        Console.WriteLine("ERROR: Unrecognized option. Please choose a valid option.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            Console.WriteLine($"An error occurred: {ex}");
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
                e - DELETE ENGINEER
                f - DELETE ALL ENGINEERS");
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
                    foreach (Engineer item in engineer.ReadAll(engineer => engineer.Id > 0))///print all content
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case "d":

                    Console.WriteLine("enter the engineer ID");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("enter the Engineer name");
                    string? name2 = Console.ReadLine();
                    Console.WriteLine("enter the Engineer email");
                    string? email2 = Console.ReadLine();
                    Console.WriteLine("enter the Engineer level");
                    EngineerLevel? level2 = EngineerLevel.Beginner;/*TODO*/
                    Console.WriteLine("enter the Engineer cost");
                    double.TryParse(Console.ReadLine(), out cost);
                    Engineer tempEngineer2 = new Engineer(id, name2, email2, level2, cost);
                    engineer.Update(tempEngineer2);
                    break;
                case "e":
                    Console.WriteLine("enter the Engineer ID");
                    int.TryParse(Console.ReadLine(), out id);
                    myId = id;
                    engineer.Delete(myId);
                    break;
                case "f":
                    engineer.DeleteAll();
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
                e - DELETE Dependence
                f - DELETE ALL Dependense");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    break;
                case "a":

                    Console.WriteLine("enter the new Dependence Id");
                    int.TryParse(Console.ReadLine(), out DependenceId);
                    Console.WriteLine("enter the new Pending Task Id");
                    int.TryParse(Console.ReadLine(), out PendingTaskId);
                    Console.WriteLine("enter the new Previous Task Id");
                    int.TryParse(Console.ReadLine(), out PreviousTaskId);
                    Dependency tempDependence = new Dependency(DependenceId, PendingTaskId, PreviousTaskId);
                    dependence.Create(tempDependence);
                    break;
                case "b":
                    Console.WriteLine("enter the Dependence ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(dependence.Read(myId));
                    break;
                case "c":
                    foreach (Dependency oItem in dependence.ReadAll(dependence => dependence.DependenceId > 0))///print all content
                    {
                        Console.WriteLine(oItem);
                    }
                    break;
                case "d":
                    Console.WriteLine("enter the new Dependence Id");
                    int.TryParse(Console.ReadLine(), out DependenceId);
                    Console.WriteLine("enter the new Pending Task Id");
                    int.TryParse(Console.ReadLine(), out PendingTaskId);
                    Console.WriteLine("enter the new Previous Task Id");
                    int.TryParse(Console.ReadLine(), out PreviousTaskId);
                    Dependency tempDependence2 = new Dependency(DependenceId, PendingTaskId, PreviousTaskId);

                    dependence.Update(tempDependence2);
                    break;
                case "e":
                    Console.WriteLine("enter the Dependence ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    dependence.Delete(myId);
                    break;
                case "f":
                    dependence.DeleteAll();
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
                e - DELETE TASK
                f - DELETE ALL TASKS");
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
                    DateTime? creationDate = null;
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

                    DO.Task tempTask = new DO.Task(id, nickName, description, creationDate, estimatedDate, startDate, numOfDays, deadLine, finishtDate, product, remarks, engineerId, RequiredLevel);
                    task.Create(tempTask);

                    break;
                case "b":
                    Console.WriteLine("enter the task ID");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(task.Read(myId));
                    break;
                case "c":
                    foreach (DO.Task item in task.ReadAll(task => task.TaskId > 0))///print all content
                    {
                        Console.WriteLine(item);
                    }
                        ;
                    break;
                case "d":
                    Console.WriteLine("enter the new task ID");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("enter the nick name");
                    string? nickName2 = Console.ReadLine();
                    Console.WriteLine("enter the description");
                    string? description2 = Console.ReadLine();
                    Console.WriteLine("enter the mile Stone");
                    bool.TryParse(Console.ReadLine(), out mileStone);
                    Console.WriteLine("enter the creation Date");
                    //  while (!DateTime.TryParse(Console.ReadLine(), out  date)) ;
                    DateTime? creationDate2 = null;
                    Console.WriteLine("enter the estimated Date");
                    DateTime? estimatedDate2 = null;
                    Console.WriteLine("enter the start Date");
                    DateTime? startDate2 = null;
                    Console.WriteLine("enter the num Of Days");
                    int.TryParse(Console.ReadLine(), out numOfDays);
                    Console.WriteLine("enter the dead Line");
                    DateTime? deadLine2 = null;
                    Console.WriteLine("enter the finisht Date");
                    DateTime? finishtDate2 = null;
                    Console.WriteLine("enter the product name");
                    string? product2 = Console.ReadLine();
                    Console.WriteLine("enter the remarks ");
                    string? remarks2 = Console.ReadLine();
                    Console.WriteLine("enter the engineer Id");
                    int.TryParse(Console.ReadLine(), out engineerId);
                    Console.WriteLine("enter the Required Level");
                    EngineerLevel? RequiredLevel2 = EngineerLevel.Beginner;

                    DO.Task tempTask2 = new DO.Task(id, nickName2, description2, creationDate2, estimatedDate2, startDate2, numOfDays, deadLine2, finishtDate2, product2, remarks2, engineerId, RequiredLevel2);
                    task.Update(tempTask2);
                    break;
                case "e":
                    Console.WriteLine("enter the Task ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    task.Delete(myId);
                    break;
                case "f":
                    task.DeleteAll();
                    break;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

}

