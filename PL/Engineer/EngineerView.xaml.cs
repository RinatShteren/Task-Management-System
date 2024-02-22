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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerView.xaml
    /// </summary>
    public partial class EngineerView : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Engineer CurrentEngineer { get; set; }
        int ID;/*if idd =0 :ADD else:UPDATE*/
        public EngineerView(int idd=0)
        {

        InitializeComponent();
            ID = idd;
            if(ID == 0)
            {
                CurrentEngineer = new BO.Engineer {
                    Id = 0
                };
            }
            else
            {
                try
                {
                    CurrentEngineer = s_bl.Engineer.Read(idd);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


            private void cbSemesterSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
       
    }
}
