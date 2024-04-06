using BO;
using DalApi;
using PL.Admin;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public BO.EngineerLevel EngineerLevel { get; set; } = BO.EngineerLevel.Beginner;
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

        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        public static readonly DependencyProperty CurrentTaskProperty =
               DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(EngineerWindow), new PropertyMetadata(null));


        public EngineerWindow(int GetId)
        {
            try
            {
                InitializeComponent();
                // CurrentEngineer = (BO.Engineer)s_bl.Engineer.ReadAll(Eng => Eng.Id == GetId);
                CurrentEngineer = s_bl.Engineer.Read(GetId);
                //CurrentTaskInList = (IEnumerable<BO.TaskInList>)s_bl.Task.Read(CurrentEngineer.Task.Id);

                //  observeListPerson = s_bl.Task.ReadAll(a => a.TaskId > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new RegisterTaskWindow(CurrentEngineer.Id).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateEngineer();

        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {

            //  BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
            CurrentTask = s_bl.Task.Read(CurrentEngineer.Task.Id);

            new TaskView(CurrentTask.TaskId).ShowDialog();
            //UpdateTask();
        }

        void UpdateEngineer()
        {
           CurrentEngineer =
               s_bl?.Engineer.Read(CurrentEngineer.Id);

        }
        void UpdateTask()
        {
            try
            {
                CurrentTask = s_bl?.Task.Read(CurrentEngineer.Task.Id);
            } // sort by ID 

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
   
}
