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
        public BO.Status Status { get; set; } = BO.Status.Scheduled;

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
            try
            {
                InitializeComponent();
                TaskList = s_bl.Task.ReadAll(/*task=>task.taskCanBeAssginToEngineer()*/).Where(task => task.TaskId > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox combo)
                {
                    Status selcted = (Status)combo.SelectedItem;
                    TaskList = s_bl?.Task.ReadAll(tsk => tsk.Status ==selcted)!;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TaskView().ShowDialog();
                UpdateTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the selected item
            //var selectedTask = (BO.TaskInList)TaskListView.SelectedItem;

            // Open TaskWindow and pass the selected task
            //  TaskWindow taskWindow = new TaskWindow(int GetId = 0);
            //taskWindow.ShowDialog();
            try
            {
                //BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
                int id =((BO.TaskInList)((ListView)sender).SelectedItem).TaskId;
                new TaskView(id).ShowDialog();
                UpdateTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        void UpdateTaskList()
        {
            try
            {
                TaskList = ((Status == BO.Status.Scheduled) ?   //?? 
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(tsk => Status == BO.Status.Scheduled)!)  //??
               .OrderBy(t => t.TaskId);
            } // sort by ID 

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}