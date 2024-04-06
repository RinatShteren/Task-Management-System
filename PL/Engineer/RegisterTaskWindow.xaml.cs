using BO;
using DO;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for RegisterTaskWindow.xaml
    /// </summary>
    public partial class RegisterTaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        int constEngineerId = 0;

        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(RegisterTaskWindow), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
        public static readonly DependencyProperty CurrentEngineerProperty =
          DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(RegisterTaskWindow), new PropertyMetadata(null));
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public static readonly DependencyProperty TaskProperty =
         DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(RegisterTaskWindow), new PropertyMetadata(null));
       
        public RegisterTaskWindow(int EngineerId =0)
        {
            constEngineerId = EngineerId;
            TaskList = s_bl.Task.ReadAll().Where(task => task.TaskId > 0);
           // TaskList = s_bl.Task.ReadAll(s_bl.TaskCanBeAssginToEngineer).Where(task => task.TaskId > 0);
            CurrentEngineer = s_bl.Engineer.Read(EngineerId);
            InitializeComponent();

        }
        private void ListViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? taskId = (sender as ListView)?.SelectedItem as BO.TaskInList;

            CurrentTask = s_bl.Task.Read(taskId.TaskId);
            CurrentEngineer.Task = new TaskInEngineer { Id = taskId.TaskId, NickName = CurrentTask.NickName };


            s_bl.Engineer.ReadAllOptionalTasksForEngineer(CurrentEngineer);
            s_bl.Engineer.AssginTaskToEngineer(CurrentEngineer);

            /* CurrentTask = s_bl.Task.Read(taskId.TaskId);//קריאת המשימה הספציפית
          
            CurrentTask.EngineerId = constEngineerId;

             *  //פה נכניס את המשימה לתוך המהנדס הספציפי.
            //בנוסף נכניס למשימה את תעודת הזהות הספציפית
            CurrentTask.EngineerId = constEngineerId;
            s_bl.Task.Update(CurrentTask);

            CurrentEngineer.Task = new TaskInEngineer { Id = taskId.TaskId, NickName = CurrentTask.NickName };
            s_bl.Engineer.Update(CurrentEngineer);
            //UpdateTaskList
            */
            // new EngineerWindow(taskId!.TaskId).ShowDialog();
            //TaskList = s_bl?.Task.ReadAll(task => task.TaskId ==taskId.TaskId).OrderBy(e => e.TaskId); // sort by ID 
            Close();
        }

       
    }
}

