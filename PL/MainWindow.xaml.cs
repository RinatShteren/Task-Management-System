using BO;
using PL.Admin;
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



        public MainWindow()
        {
            try
            {
                InitializeComponent();
                //_ = s_bl.Clock;
                DataContext = this; // Set DataContext to the MainWindow itself
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnOPEN_Click(object sender, RoutedEventArgs e)
        {

            //if (s_bl.UserLogin.UserExist(User))
            //{
            try
            {
                if (User.Password == 1234 && User.UserId == 111111111)
                {
                    new ManagerView().Show();
                }
                else
                {
                    string? userId = UserIdBox?.Text;
                    new EngineerWindow(int.Parse(userId)).ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EngineerView().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /** clock **/

        // CurrentTime property
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Dependency property for CurrentTime
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register(nameof(CurrentTime), typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

        private void AddDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Call BL method to increment day
                s_bl.AdvanceTimeByDay(1);
                CurrentTime = s_bl.Clock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void AddMonth_Click(object sender, RoutedEventArgs e)
        {
            try {s_bl.AdvanceTimeByMonth(1);
                CurrentTime = s_bl.Clock;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddYear_Click(object sender, RoutedEventArgs e)
        {
            try {s_bl.AdvanceTimeByYear(1);
                CurrentTime = s_bl.Clock;
            }
            
              catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializationClock_Click(object sender, RoutedEventArgs e)
        {
            try { s_bl.InitializeTime();
                CurrentTime = s_bl.Clock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}