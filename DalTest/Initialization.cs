namespace DalTest;
using DalApi;
using DO;
using System;




public static class Initialization
{

    private static IDal? s_dal = Factory.Get; //stage 2

    private static readonly Random s_rand = new();
    private const int MIN_ID = 200000000;
    private const int MAX_ID = 400000000;

    public static void Do() //stage 4
    {

        s_dal = Factory.Get; //stage 4
        createEngineers();
        createTasks();
        createDependences();
        createUsers();
    }

    public static void Reset()//stage 5
    {
        const int startDependenceId = 1;
        const int startTaskId = 1;
        s_dal = Factory.Get;
        s_dal.Schedule.ResetDep();

        deleteEngineers();
        deleteTasks();
        deleteDependences();
        deleteUsers();
    }
    private static void createEngineers()
    {


        string[] engineerNames =   //רשימה של שמות מהנדסים לאיתחול בהמשך
        {
        "Dani Levi", "Eli Amar", "Neria Cohen","Ariela Levin", "Dina Klein"
        };
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            int _id = random.Next(200000000, 400000000); //מגריל תז רנדומלי בין 2000000 ל4000000
            string? _name = engineerNames[i];
            string? _email = _name[0] + _name[1] + "@gmail.com"; //אימייל שמורכב מהשם של המהנדס
            //EngineerLevel? _engineerLevel = EngineerLevel.Beginner;
            EngineerLevel _engineerLevel = (EngineerLevel)new Random().Next(Enum.GetValues(typeof(EngineerLevel)).Length); //נגדיר מס רנדומלי שייתן רמת מהנדס רנדומלית מתוך האינם
            double _cost = s_rand.Next(200, 1000); //נגריל מס רנדומלי לעלות המהנדס
            Engineer newEng = new(_id, _name, _email, _engineerLevel, _cost); //נגדיר מהנדס חדש זמני עם הערכים שאיתחלנו

            s_dal!.Engineer.Create(newEng);

        }
    }

    private static void createUsers()
    {
        s_dal.User.Create(new User(1234, 11111111));
        s_dal.User.Create(new User(1111, 222222222));

        var engineers = s_dal.Engineer.ReadAll().ToList();

        foreach (var engineer in engineers)
            s_dal.User.Create(new User(new Random().Next(2000, 4000), engineer.Id));
    }
    private static void createTasks()
    {

        string[] TaskNickName = //רשימה של כינויים של משימות
         {
       "Project Alpha", "Data Analysis", "Budget Planning", "Website Redesign", "Marketing Campaign",
       "Product Launch", "Meeting Prep", "Client Presentation", "Research Proposal", "Content Creation",
       "Event Coordination", "Software Development", "Customer Support", "Inventory Management", "Social Media Strategy",
       "Training Session", "Financial Reporting", "Sales Pitch", "Quality Assurance", "Team Building"
        };
        string[] TaskDescription = //רשימה של תיאורים של משימות
        {
        "Drafting Project Proposal", "Analyzing Sales Data", "Creating Budget Report", "Redesigning Website Layout",
        "Executing Marketing Campaign", "Planning Product Launch Event", "Preparing Meeting Agenda", "Designing Client Presentation",
        "Conducting Research Proposal", "Producing Content for Blog", "Coordinating Event Logistics", "Developing Software Features",
        "Providing Customer Support", "Managing Inventory Updates", "Crafting Social Media Strategy", "Conducting Training Session",
        "Compiling Financial Reports", "Delivering Sales Pitch", "Ensuring Quality Assurance", "Facilitating Team Building Activities"
        };
        string[] Products = //רשימה של תיאורים של תוצרים
        {
        "CodeCraft IDE", "DataForge Analytics Suite", "SecureGuard Cybersecurity Platform", "AgileFlow Project Management Tool",
        "TechScan Code Review System", "CloudSync File Hosting Service", "BugShield QA Testing Suite","DevOpsNavigator CI/CD Pipeline",
        "AIInsight Data Analysis Software", "AutomatePro Workflow Automation Tool", "MobileGuard App Security Suite",
        "NetworkPulse Monitoring Solution", "SmartQuery Database Optimization", "ScriptMaster Automation Framework",
        "VirtualizeIT Virtualization Platform", "EncryptPro Data Encryption Software", "BotAssist Chatbot Development Kit",
        "IoTShield Device Security Suite", "IntelliSync Integration Middleware", "SprintX Agile Development Framework"
        };
        string[] Remarks =//רשימה של הערות אפשריות
        {
            "Met project deadlines on time.","Enhanced UI based on testing.", "Optimized for various devices.","Implemented robust error handling.",
            "Streamlined QA testing processes.","Introduced automated testing.", "Documented detailed release notes.", "Deployed updates with no downtime.",
            "Enabled feature toggles for control.", "Performed regular security audits.", "Collaborated for seamless API integration.",
            "Optimized queries for scalability.","Boosted system performance with caching.", "Conducted post-implementation review."
        };

        for (int i = 0; i < 20; i++)
        {
            int num = s_rand.Next(0, 20);

            string _TaskNickName = TaskNickName[i];//כל פעם תוגרל רנדומלית משימה ותיאור
            string _Description = TaskDescription[i];
            bool _MileStone = false;
            if (i % 2 == 0) //כדי לגוון שלפעמים יהיה טרו ולפעמים פאלס
                _MileStone = true;
            int? _NumOfDays = s_rand.Next(1, 4); //ההפרש בין התאריך המשוער לדד ליין
            string? _Product = Products[num]; //כל פעם יוגרל רנדומלית תוצר 
            string? _Remarks = Remarks[num % 2]; //כל פעם יוגרל רנדומלית הערה מהרשימה 
            num = s_rand.Next(10, 40);
            DateTime? _CreationDate = DateTime.Now.AddDays(num); //בכל איטרציה יוגרל תאריך משימה
            //DateTime? _EstimatedDate = DateTime.Now.AddDays(num + 2); //תאריך התחלה
            //DateTime? _StartDate = DateTime.Now.AddDays(num + 3);  //יתחיל יום אחרי המשוער
            //DateTime? _DeadLine = DateTime.Now.AddDays(num + 3 + i); //שיחקתי עם זה קצת שיהיה שונה אבל עקבי

           // DateTime? _FinishtDate = DateTime.Now.AddDays(num + 2 + i); //תמיד לפני הדד ליין

            DateTime? _EstimatedDate = null; //תאריך התחלה
            DateTime? _StartDate = null;  //יתחיל יום אחרי המשוער
            DateTime? _DeadLine = null; //שיחקתי עם זה קצת שיהיה שונה אבל עקבי

            DateTime? _FinishtDate = null; //תמיד לפני הדד ליין
            var engineesr = s_dal!.Engineer.ReadAll(a => a.Id > 0);

            //int EnginnerId =
            //EngineerLevel EngineerLevel = 
            EngineerLevel _RequiredLevel = (EngineerLevel)new Random().Next(Enum.GetValues(typeof(EngineerLevel)).Length); //נגדיר מס רנדומלי שייתן רמת מהנדס רנדומלית מתוך האינם

            Task newTsk = new Task(0, _TaskNickName, _Description, _CreationDate, _EstimatedDate, _StartDate,
                _NumOfDays, _DeadLine, _FinishtDate, _Product, _Remarks, 0, _RequiredLevel); //נגדיר משימה זמנית
            s_dal!.Task.Create(newTsk);             //נכניס אותה לבסיס נתונים

        }

    }
    private static void createDependences()
    {
        int depend = 0;
        int dependon = 0;
        List<Task> tasks = s_dal.Task.ReadAll().ToList();
        HashSet<Dependency> depenendencies = new();
        for (int i = 5; i < tasks.Count() * 2; i++)
        {
            switch (i)
            {


                case int x when x < 10:
                    dependon = s_rand.Next(0, 5);
                    depend = s_rand.Next(5, 11);
                    dependon = tasks[dependon].TaskId;
                    depend = tasks[depend].TaskId;
                    break;

                case int x when x < 20:
                    dependon = s_rand.Next(5, 11);
                    depend = s_rand.Next(11, 20);//                    depend = s_rand.Next(11, 21);
                    dependon = tasks[dependon].TaskId;
                    depend = tasks[depend].TaskId;
                    break;

                //case int x when x < 40:
                //    dependon = s_rand.Next(11, 20);//                    dependon = s_rand.Next(11, 21);
                //    depend = s_rand.Next(21, 20);//                    depend = s_rand.Next(21, 40);
                //    dependon = tasks[dependon].TaskId;
                //    depend = tasks[depend].TaskId;
                //    break;

                default:
                    return;

            }
            Dependency dep = new Dependency(
                DependenceId: 0,
                PendingTaskId: depend,
                PreviousTaskId: dependon);

            if (!depenendencies.Contains(dep))
                s_dal.Dependency.Create(dep);
        }

    }

    private static void deleteEngineers()
    {
        s_dal!.Engineer.DeleteAll();
    }
    private static void deleteTasks()
    {
        s_dal!.Task.DeleteAll();
    }
    private static void deleteDependences()
    {
        s_dal!.Dependency.DeleteAll();
    }
    private static void deleteUsers()
    {
        s_dal!.User.DeleteAll();
    }
}