using System.Windows;
using System.Windows.Controls;
using System.Linq;
namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        RELICEntities db = new RELICEntities();
   
        public string currentPassword;
        public string username;
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            user.userpassword = currentPassword;
        
            // detrmine that the oldPassword entered is valid
            if (tbxOldPassword.Password == currentPassword) 
            {
                // confirm new passowrd is confirmed corretcly
                if (tbxNewPassword.Password == tbxConfirmPassword.Password)
                {
                    
                    foreach (var record in db.Users.Where (t=>t.username == username) )
                    {
                        record.userpassword = tbxConfirmPassword.Password.Trim();
                    }
                    int Success = db.SaveChanges();
                    if (Success == 1)
                    {
                        // after we change password we set the currentPassword to the modified password.
                        currentPassword = tbxConfirmPassword.Password.Trim();
                        MessageBox.Show("Password has been successfully updated.  ");                       
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Problem changing Password ");
                    }
                }
                else
                {
                    MessageBox.Show("Mismatch occured when confirming new password");

                }
            }
            else
            {
                MessageBox.Show("Current Password Entered is incorrect");

            }



        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
