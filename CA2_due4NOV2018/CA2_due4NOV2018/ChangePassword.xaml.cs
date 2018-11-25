using System.Windows;
using System.Windows.Controls;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbxRiderName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
