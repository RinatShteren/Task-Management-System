using DalApi;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        List<BO.Task> listPerson;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.TaskInList> CurrentTaskInList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("CurrentTaskInList", typeof(IEnumerable<BO.TaskInList>), typeof(EngineerWindow), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
    public static readonly DependencyProperty CurrentEngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

      
        public EngineerWindow(int GetId)
        {
            InitializeComponent();
            // CurrentEngineer = (BO.Engineer)s_bl.Engineer.ReadAll(Eng => Eng.Id == GetId);
            CurrentEngineer = s_bl.Engineer.Read(GetId);
            //CurrentTaskInList = (IEnumerable<BO.TaskInList>)s_bl.Task.Read(CurrentEngineer.Task.Id);

            //  observeListPerson = s_bl.Task.ReadAll(a => a.TaskId > 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new RegisterTaskWindow(CurrentEngineer.Id).ShowDialog();

         
        }
    }
}
