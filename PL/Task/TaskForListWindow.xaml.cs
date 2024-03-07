using BO;
using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskForListWindow.xaml
    /// </summary>
    public partial class TaskForListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Stage Stage { get; set; } = BO.Stage.Planning;
   
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
                typeof(TaskForListWindow), new PropertyMetadata(null));

        public TaskForListWindow()
        {
            InitializeComponent();
            TaskList = s_bl.Task.ReadAll().Where(task => task.TaskId > 0);
        }

        void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = ((Stage == BO.Stage.Planning) ?   //?? 
               s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(tsk => Stage == BO.Stage.Planning)!)  //??
               .OrderBy(t => t.TaskId); // sort by ID 
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new TaskView().ShowDialog();
            UpdateTaskList();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the selected item
            //var selectedTask = (BO.TaskInList)TaskListView.SelectedItem;

            // Open TaskWindow and pass the selected task
            //  TaskWindow taskWindow = new TaskWindow(int GetId = 0);
            //taskWindow.ShowDialog();

            BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskView(task!.TaskId).ShowDialog();
            UpdateTaskList();
        }

        void UpdateTaskList()
        {

            TaskList = ((Stage == BO.Stage.Planning) ?   //?? 
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(tsk => Stage == BO.Stage.Planning)!)  //??
               .OrderBy(t => t.TaskId); // sort by ID 
                                        //}
                                        //option for the user to sort the list of tasks according to their stage
        }
    }
}