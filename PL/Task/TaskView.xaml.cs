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
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        //private BO.TaskInList selectedTask;
        int AddOrUpdate;/*if idd =0 :ADD else:UPDATE*/
        public static readonly DependencyProperty CurrentTaskProperty =
           DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskView), new PropertyMetadata(null));
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public TaskView(int GetId = 0)
        {
            InitializeComponent();
            if (GetId == 0)
            {

                AddOrUpdate = 0;
                CurrentTask = new BO.Task() { TaskId = 0 };
            }
            else
            {
                AddOrUpdate = 1;
                try
                {
                    CurrentTask = s_bl.Task.Read(GetId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // this.selectedTask = task;

            // DataContext = this; // Set DataContext to this instance of TaskWindow
        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddOrUpdate == 0)
                {
                    s_bl.Task.Create(CurrentTask);
                }
                else
                {
                    s_bl.Task.Update(CurrentTask);
                }
            }

            catch (BO.BlNotVaildException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Close();
        }


    }
}
