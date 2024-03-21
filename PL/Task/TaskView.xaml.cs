﻿using PL.Engineer;
using System;
using System.Windows;
using System.Windows.Controls;

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
        /////// <summary>
        /////// /////////////////////////////
        /////// </summary>
        ////public static readonly DependencyProperty CurrentDepProperty =
        //// DependencyProperty.Register("CurrentDep", typeof(List<BO.TaskInList>), typeof(TaskView), new PropertyMetadata(null));
        ////public List<BO.TaskInList> CurrentDep
        ////{
        ////    get { return (List<BO.TaskInList>)GetValue(CurrentDepProperty); }
        ////    set { SetValue(CurrentDepProperty, value); }
        ////}
        /////// <summary>
        /////// ////////////////
        /////// </summary>
        /////// <param name="GetId"></param>
        
        public TaskView(int GetId = 0)
        {
            try
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
                    CurrentTask = s_bl.Task.Read(GetId);
                }
                //CurrentDep = CurrentTask.Dependencies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Your event handling code here
            // This method will be called whenever the selection in the ListBox changes
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
