﻿using System.Windows;
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
       // ChangePassword changepassword = new ChangePassword();
        public string currentPassword;
        public string username;
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            user.userpassword = currentPassword;
         //   db.Entry(user).State = System.Data.Entity.EntityState.Added;

            if (tbxOldPassword.Password == currentPassword) 
            {
                if (tbxNewPassword.Password == tbxConfirmPassword.Password)
                {
                    //foreach (var record in db.Competitions.Where(t => t.competition_id == competition_id))
                        //   db.Entry(user).State = System.Data.EntityState.Modified;
                    foreach (var record in db.Users.Where (t=>t.username == username) )
                    {
                        record.userpassword = tbxConfirmPassword.Password.Trim();
                    }
                    int Success = db.SaveChanges();
                    if (Success == 1)
                    {
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
