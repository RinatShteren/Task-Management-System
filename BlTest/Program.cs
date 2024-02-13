using BlApi;
using BO;
using DalApi;
using System;
using System.Reflection.Emit;
namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N) ");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();

            int num = 1;
            while (num != 0)
            { 
                Console.WriteLine(@"Hello!
                Enter your choice:
                0 - exit
                1 - test Task
                2 - test Engineer 
                3 - automaticSchedule ");

                // Read user input
                string? option = Console.ReadLine();
                
               // Check if the input is a valid integer
                bool isNumeric = int.TryParse(option, out num);
              
                // Handle non-numeric input
                if (!isNumeric)
                {
                    Console.WriteLine("ERROR: Invalid input. Please enter a number.");
                    break;
                }/*להעביר את זה מכאן אם יש זמן*/
                try
                {
                    switch (num)
                    {
                        case 0: break;
                        case 1: taskMenu(); break;
                        case 2: engineerMenu(); break;
                        case 3:
                            DateTime startOfProject = DateTime.Parse(getString("Please enter start-date of the project"));
                            if (DalApi.Factory.Get.saveStartandFinishDatestoFile("data-config", "startDate", startOfProject))
                                s_bl.automaticSchedule();
                            else throw new BO.BlAlreadyExistsException($"The date is already exsist");
                            break;
                        default:
                            // Handle unrecognized input
                        Console.WriteLine("ERROR: Unrecognized option. Please choose a valid option.");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        Console.WriteLine("Dal Exception:");

                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Message);
                }

            }


        }

        private static void taskMenu()
        {

            string? choice;

            try
            {
                Console.WriteLine(@"test order:
                Enter your choice:
                0 - EXIT 
                a - ADD TASK
                b - DELETE TASK
                c - GET TASK BY ID
                d - GET TASKS LIST
                e - UPDATE TASK
                f - UPDATE DATE
                g - RETURN ");
                string? option = Console.ReadLine();
                switch (option)
                {
                    case "0": return;

                    case "a":
                        string? name = (getString("enter Name")), descreption = getString("enter description"),
                        product = getString("enter product"), notes = getString("enter notes");
                        TimeSpan? duration = TimeSpan.Parse(getString("enter duration"));
                        EngineerLevel difficulty = (EngineerLevel)int.Parse(getString("enter level"));

                        List<BO.TaskInList> prevTasks = new List<BO.TaskInList>();

                        int tempID = int.Parse(getString("enter id of previous task or 0 to finish"));
                        while (tempID != 0)
                        {
                            BO.Task? fullTask = s_bl.Task.Read(tempID);
                            if (fullTask == null)
                            {
                                throw new BO.BlDoesNotExistException($"Task with ID={tempID} does Not exist");
                            }

                            BO.TaskInList temp = new BO.TaskInList()
                            {
                                Id = tempID,
                                Description = fullTask.Description,
                                Name = fullTask.Name,
                                Status = (BO.Status?)fullTask.Status

                            };
                            prevTasks.Add(temp);
                            tempID = Convert.ToInt16(getString("enter id of previous task or 0 to finish"));
                        }

                        BO.Task task = new BO.Task()
                        {
                            Name = name,
                            Description = descreption,
                            Creation = DateTime.Now,
                            Status = BO.Status.Unscheduled,
                            Links = prevTasks,

                            Duration = duration,
                            Product = product,
                            Notes = notes,
                            Difficulty = difficulty
                        };


                        int newID = s_bl.Task.Add(task);
                        Console.WriteLine(newID);
                        break;
                    case "delete":
                        int idToDelete = int.Parse(getString("enter the id of the task to delete"));
                        s_bl.Task.Delete(idToDelete);
                        //s_bl.Task.Read(idToDelete); //התוצאה צריכה להיות שגיאה שנתפסת
                        break;
                    case "read":
                        int idToRead = int.Parse(getString("enter the id of the task to read"));
                        Console.WriteLine(s_bl.Task.Read(idToRead));
                        break;
                    case "read all":
                        foreach (var item in s_bl.Task.ReadAll())
                            Console.WriteLine(item);

                        break;
                    case "update":
                        int id = int.Parse(getString("enter id"));
                        name = (getString("enter Name")); descreption = getString("enter description");
                        product = getString("enter product"); notes = getString("enter notes");
                        duration = TimeSpan.Parse(getString("enter duration"));
                        difficulty = (Level)int.Parse(getString("enter level"));

                        prevTasks = new List<BO.TaskInList>();

                        tempID = int.Parse(getString("enter id of previous task or 0 to finish"));
                        while (tempID != 0)
                        {
                            BO.Task tempTask = s_bl.Task.Read(tempID) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
                            BO.TaskInList temp = new BO.TaskInList()
                            {
                                Id = tempID,
                                Description = tempTask.Description,
                                Name = tempTask.Name,
                                Status = (BO.Status?)tempTask.Status
                            };
                            prevTasks.Add(temp);
                            tempID = int.Parse(getString("enter id of previous task or 0 to finish"));
                        }

                        BO.Task taskToUpdate = new BO.Task()
                        {
                            Id = id,
                            Name = name,
                            Description = descreption,
                            Creation = DateTime.Now,
                            Status = BO.Status.Unscheduled,
                            Links = prevTasks,

                            Duration = duration,
                            Product = product,
                            Notes = notes,
                            Difficulty = difficulty
                        };

                        s_bl.Task.Update(taskToUpdate);
                        //check:
                        Console.WriteLine(s_bl.Task.Read(taskToUpdate.Id));
                        break;
                    case "update date":
                        id = int.Parse(getString("enter id of task to update date"));
                        DateTime date = DateTime.Parse(getString("enter the new date"));
                        s_bl.Task.UpdateDate(id, date);
                        //check:
                        Console.WriteLine(s_bl.Task.Read(id));
                        break;

                    default:
                        {
                            Console.WriteLine("invalid input, please enter again");
                            choice = Console.ReadLine();
                            break;
                        }
                }

                return;

            }
            }
        private static void engineerMenu()
        {
           
            Console.WriteLine(@"test order:
                Enter your choice:
                0 - EXIT MANU
                a - ADD ENGINEER
                b - DELETE ENGINEER 
                c - GET ENGINEER BY ID
                d - GET ENGINEERS LIST
                e - UPDATE ENGINEER");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "0": return;
                case "a":
                    BO.Engineer engineer = new BO.Engineer()
                    {
                        Id = int.Parse(getString("enter id\n")),
                        Name = getString("enter name"),
                        Email = getString("enter email"),
                        Level = (EngineerLevel)int.Parse(getString("enter level")),
                        Cost = double.Parse(getString("enter cost"))
                    };
                    Console.WriteLine(s_bl.Engineer.AddEngineer(engineer));
                    break;
                case "b":
                    int id = int.Parse(getString("enter id to delete"));
                    s_bl.Engineer.Delete(id);
                    break;
                case "c":
                    id = int.Parse(getString("enter id to read"));
                    Console.WriteLine(s_bl.Engineer.Read(id));
                    break;
                case "d":
                    foreach (var item in s_bl.Engineer.ReadAll(null))
                        Console.WriteLine(item);
                    break;
                case "update":
                    BO.Engineer eng = new BO.Engineer()
                    {
                        Id = int.Parse(getString("enter id\n")),
                        Name = getString("enter name"),
                        Email = getString("enter email"),
                        Level = (Level)int.Parse(getString("enter level")),
                        Cost = double.Parse(getString("enter cost"))
                    };

                    if (getString("do you want add task to the engineer? enter Y/N") == "Y")
                    {
                        id = Convert.ToInt16(getString("enter id of task"));
                        string name = getString("enter name of the task");
                        BO.TaskInEngineer temp = new BO.TaskInEngineer() { Id = id, Name = name };
                        eng.Task = temp;
                    }

                    s_bl.Engineer.Update(eng);
                    break;
          
                default:
                    {
                        Console.WriteLine("doesnt valid input, please enter again");
                        choice = Console.ReadLine();
                        return;
                    }
            }

            return;
        }


        private static string getString(string s)
        {
            Console.WriteLine(s);
            return Console.ReadLine();
        }


    }
}
