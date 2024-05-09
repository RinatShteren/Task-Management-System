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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for GanttW.xaml
    /// </summary>
    public partial class GanttW : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public List<BO.Task> ListOfTasks
        {
            get { return (List<BO.Task>)GetValue(ListOfTasksProperty); }
            set { SetValue(ListOfTasksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListOfTasks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListOfTasksProperty =
            DependencyProperty.Register("ListOfTasks", typeof(List<BO.Task>), typeof(GanttW), new PropertyMetadata(null));

        public GanttW()
        {           InitializeComponent();
            try
            {
 ListOfTasks = new List<BO.Task>();

            List<BO.TaskInList> listT = s_bl.Task.ReadAll().ToList();
            ListOfTasks = (from item in listT
                           select s_bl.Task.Read(item.TaskId)).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
