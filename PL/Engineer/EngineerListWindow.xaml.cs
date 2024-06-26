﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public EngineerListWindow()
        {
            try
            {
                InitializeComponent();
                EngineerList = s_bl.Engineer.ReadAll(engineer => engineer.Id > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }



        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerLevel EngineerLevel { get; set; } = BO.EngineerLevel.Beginner;

        //option for the user to sort the list of engineers according to their level
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                EngineerList = ((EngineerLevel == BO.EngineerLevel.Beginner) ?   //?? 
                   s_bl?.Engineer.ReadAll(null)! : s_bl?.Engineer.ReadAll(eng => eng.Level == EngineerLevel)!)  //??
                   .OrderBy(e => e.Id); // sort by ID 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                new EngineerView().ShowDialog();
                UpdateEngineerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
                // if (engineer == null)
                // throw new Excption("engineer is null");
                new EngineerView(engineer!.Id).ShowDialog();
                UpdateEngineerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void UpdateEngineerList()
        {
            try
            {
                EngineerList = ((EngineerLevel == BO.EngineerLevel.Beginner) ? //??
                    s_bl?.Engineer.ReadAll(null)! : s_bl?.Engineer.ReadAll(eng => eng.Level == EngineerLevel)!)
                    .OrderBy(e => e.Id); // sort by ID 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}
