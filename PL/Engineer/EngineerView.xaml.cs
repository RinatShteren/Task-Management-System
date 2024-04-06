using BO;
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
        int AddOrUpdate;/*if idd =0 :ADD else:UPDATE*/


        public static readonly DependencyProperty CurrentEngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerView), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
    

        public EngineerView(int GetId=0)
        {
            try
            {
                InitializeComponent();
                // ID = idd;
                if (GetId == 0)
                {

                    AddOrUpdate = 0;
                    CurrentEngineer = new BO.Engineer() { Id = 0 };
                    // SetValue(CurrentEngineerProperty, new BO.Engineer ());

                }
                else
                {
                    AddOrUpdate = 1;

                    CurrentEngineer = s_bl.Engineer.Read(GetId);
                }
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
                    s_bl.Engineer.AddEngineer(CurrentEngineer);
                }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
