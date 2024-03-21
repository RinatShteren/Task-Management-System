using BO;
using PL.Engineer;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public ManagerView()
        {
   
                InitializeComponent();

        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Are you shoure you want to initional data?");
                s_bl.InitalizingBD();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Are you shoure you want to reset data?");
                s_bl.ResetDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEngineerList_Click(object sender, RoutedEventArgs e)
        {
            try { new EngineerListWindow().Show(); }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTaskList_Click(object sender, RoutedEventArgs e)
        {
            try { new TaskForListWindow().Show(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateSelectionForm dateSelectionForm = new DateSelectionForm();
                dateSelectionForm.ShowDialog();

                DateTime selectedDate = dateSelectionForm.SelectedDate;
                               
                s_bl.Schedule.StartProject = dateSelectionForm.SelectedDate;
                MessageBox.Show($"The date you choose: {selectedDate.ToShortDateString()}");

                s_bl.Task.CalculateCloserStartDateForAllTasks();
                s_bl.Task.EnginnerToTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonGuntt_Click(object sender, RoutedEventArgs e)
        {
            try { new GanttW().Show(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

