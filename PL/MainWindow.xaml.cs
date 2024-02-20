﻿using PL.Engineer;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }
       
        private void  btnInit_Click(object sender, RoutedEventArgs e)
        { MessageBox.Show("Are you shoure you want to initional data?");
            s_bl.InitalizingBD();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you shoure you want to reset data?");
            s_bl.ResetDB();
        }
        
    }
}