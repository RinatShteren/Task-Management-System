using BO;
using PL.Engineer;
using PL.Task;
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


        public User User
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register(nameof(User), typeof(User), typeof(MainWindow), new PropertyMetadata(new User()));


        public MainWindow() => InitializeComponent();
  
        private void btnOPEN_Click(object sender, RoutedEventArgs e)
        {

            //if (s_bl.UserLogin.UserExist(User))
            //{
               
                if (User.Password == 1234 && User.UserId == 111111111)
                {
                    new EngineerListWindow().Show();
                }
                else
                {
                string? userId = UserIdBox?.Text;
                new EngineerWindow(int.Parse(userId)).ShowDialog();

            }

            //}



        }
    
    
     
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        { new TaskForListWindow().Show(); }
       
        private void  btnInit_Click(object sender, RoutedEventArgs e)
        { MessageBox.Show("Are you shoure you want to initional data?");
            s_bl.InitalizingBD();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you shoure you want to reset data?");
            s_bl.ResetDB();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new EngineerView().Show();
        }
    }
}