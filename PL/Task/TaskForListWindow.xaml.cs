﻿using BO;
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
        public TaskForListWindow()
        {
            InitializeComponent();
            TaskList = s_bl.Task.ReadAll().Where(task => task.TaskId > 0);
        }
        
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
                typeof(TaskForListWindow), new PropertyMetadata(null));

        public BO.Stage Stage { get; set; } = BO.Stage.Planning;

        //option for the user to sort the list of tasks according to their stage
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = ((Stage == BO.Stage.Planning) ?   //?? 
               s_bl?.Engineer.ReadAll(null)! : s_bl?.Engineer.ReadAll(tsk => tsk.Stage == Planning)!)  //??
               .OrderBy(t => t.Id); // sort by ID 
        }

    }
}