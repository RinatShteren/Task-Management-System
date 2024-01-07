
namespace DalTest;
using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;

public static class Initialization
{
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependence? s_dalDependence; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();


    private static void createEngineers()
    {
        long[] engineerId = //רשימה של תעודות זהות לאיתחול בהמשך, ביקשו שהתז יהיה אינט אבל זה לא עבד 
       {
        4312345678, 212345678, 312345678,112345678, 112345678
        };

        string[] engineerNames =   //רשימה של שמות מהנדסים לאיתחול בהמשך
        {
        "Dani Levi", "Eli Amar", "Neria Cohen","Ariela Levin", "Dina Klein"
        };

        for (int i = 0; i < 5; i++) 
        {
            long _id = engineerId[i];
            string? _name = engineerNames[i];
            string? _email = _name[0] + _name[1] + "@gmail.com"; //אימייל שמורכב מהשם של המהנדס
            //EngineerLevel? _engineerLevel = EngineerLevel.Beginner;
            EngineerLevel _engineerLevel = (EngineerLevel)new Random().Next(Enum.GetValues(typeof(EngineerLevel)).Length); //נגדיר מס רנדומלי שייתן רמת מהנדס רנדומלית מתוך האינם
            double _cost = s_rand.Next(200, 1000); //נגריל מס רנדומלי לעלות המהנדס
            Engineer newEng = new(_id, _name, _email, _engineerLevel, _cost); //נגדיר מהנדס חדש זמני עם הערכים שאיתחלנו
            s_dalEngineer!.Create(newEng); //נכניס אותו לבסיס הנתונים
        }
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
        long[] engineerId = //רשימה של תעודות זהות של מהנדסים  
       {
        4312345678, 212345678, 312345678,112345678, 112345678
        };
        for (int i = 0; i < 20; i++)
        {
            int num = s_rand.Next(0, 20);
            string _TaskNickName = TaskNickName[num];//כל פעם תוגרל רנדומלית משימה ותיאור
            string _Description = TaskDescription[num];
            bool _MileStone = false;
            if (i % 2 == 0) //כדי לגוון שלפעמים יהיה טרו ולפעמים פאלס
                _MileStone = true;
            DateTime? _CreationDate = DateTime.Now.AddDays(num); //בכל איטרציה יוגרל תאריך משימה
            DateTime? _EstimatedDate = DateTime.Now.AddDays(num + 2); //תאריך התחלה
            DateTime? _StartDate = DateTime.Now.AddDays(num + 3);  //יתחיל יום אחרי המשוער
            DateTime? _DeadLine = DateTime.Now.AddDays(num + 3 + i); //שיחקתי עם זה קצת שיהיה שונה אבל עקבי
            DateTime? _FinishtDate = DateTime.Now.AddDays(num + 2 + i); //תמיד לפני הדד ליין
            int? _NumOfDays = i + 1; //ההפרש בין התאריך המשוער לדד ליין
            string? _Product = Products[num]; //כל פעם יוגרל רנדומלית תוצר 
            string? _Remarks = Remarks[num]; //כל פעם יוגרל רנדומלית הערה מהרשימה 
            long _EngineerId = engineerId[i % 5]; //יגריל תז של מהנדס מהרשימה
            EngineerLevel _RequiredLevel = (EngineerLevel)new Random().Next(Enum.GetValues(typeof(EngineerLevel)).Length); //נגדיר מס רנדומלי שייתן רמת מהנדס רנדומלית מתוך האינם

            Task newTsk = new Task(0, _TaskNickName, _Description, _MileStone, _CreationDate, _EstimatedDate, _StartDate,
                _NumOfDays, _DeadLine, _FinishtDate, _Product, _Remarks, _EngineerId, _RequiredLevel); //נגדיר משימה זמנית
            s_dalTask!.Create(newTsk); //נכניס אותה לבסיס נתונים
        }
        
    }
    private static void createDependences()
    {
        for (int i = 0; i < 40; i++)
        {
            int _PendingTaskId = s_rand.Next(1000, 1020); //משימה רנדומלית מ20 המשימות הקיימות
            int temp = s_rand.Next(1000, 1020); 
            while (temp < _PendingTaskId) // נרצה שהמשימה הקודמת תיהיה עם דד ליין מוקדם יותר מהמשימה התלויה בה
                temp = s_rand.Next(1000, 1020);
            int _PreviousTaskId = temp;
            Dependence newDpns = new Dependence(0, _PendingTaskId, _PreviousTaskId); //נגדיר תלות זמנית
            s_dalDependence!.Create(newDpns); //נכניס אותה לבסיס נתונים
        }

    }

}
