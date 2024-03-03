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

namespace PL
{
    /// <summary>
    /// Interaction logic for EnteryWindow.xaml
    /// </summary>
    public partial class EnteryWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty CurrentEngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerView), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }
        public EnteryWindow(int GetId = 0)
        {
            InitializeComponent();
        }

        private void btnOPEN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Engineer.Read(CurrentEngineer!.Id).ToString == null)//its meens THE BOS come
                {
                    //open meneger page
                }

                else
                {
                    //open engineer page
                }
            }
            catch
            { }
        }
    }
}
