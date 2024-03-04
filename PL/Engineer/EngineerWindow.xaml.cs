using DalApi;
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

        public static readonly DependencyProperty observeListTaskProperty =
            DependencyProperty.Register("observeListPerson", typeof(ObservableCollection<BO.Task>), typeof(MainWindow), new PropertyMetadata(null));

        public ObservableCollection<BO.Task> observeListPerson
        {
            get { return (ObservableCollection<BO.Task>)GetValue(observeListTaskProperty); }
            set { SetValue(observeListTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public EngineerWindow(int GetId)
        {
            InitializeComponent();
            CurrentEngineer = s_bl.Engineer.Read(GetId);

          //  observeListPerson = s_bl.Task.ReadAll(a => a.TaskId > 0);
        }
    }
}
