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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for DateSelectionForm.xaml
    /// </summary>
    public partial class DateSelectionForm : Window
    {
        public DateTime SelectedDate { get; private set; }

        public DateSelectionForm()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            // בדיקה שהמשתמש הכניס תאריך חוקי
            if (DateTime.TryParse(pic.Text, out DateTime selectedDate))
            {
                SelectedDate = selectedDate;
            
            }
            else
            {
                MessageBox.Show("date is not valid");
            }
            Close();
        }
    }
}
