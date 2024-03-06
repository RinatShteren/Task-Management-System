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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private BO.TaskInList selectedTask;

        public TaskWindow(BO.TaskInList task)
        {
            InitializeComponent();

            this.selectedTask = task;

            DataContext = this; // Set DataContext to this instance of TaskWindow

        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
    //    public BO.Task Task
    //    {
    //        get { return (BO.Task)GetValue(CurrentTaskProperty); }
    //        set { SetValue(CurrentTaskProperty, value); }
    //    }

    //    public static readonly DependencyProperty CurrentTaskProperty =
    //        DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
    
}
