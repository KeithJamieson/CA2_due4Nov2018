using RelicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for LoginRegister.xaml
    /// </summary>

    public partial class LoginRegister : Window
    {
        RegisterProcess registerProcess = new RegisterProcess();
        LoginProcess loginProcess = new LoginProcess();
        //initialise variables for later user within c# 
        string DRgrade;
        string SJgrade;
        string XCgrade;
        int airc_id = 0;
        int club_id = 0;  //needs to be here as needed for registerMember Method.
        //string connectionString = "metadata=res://*/RelicModel.csdl|res://*/RelicModel.ssdl|res://*/RelicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=localhost;initial catalog=RELIC;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'";
        // set up access to database entities
        RELICEntities db = new RELICEntities();

        //List of riding clubs
        List<Club> RidingClub = new List<Club>();

        Boolean close = false;
        public string activeTab;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //populate ComboBox for Riding clubs with all existing clubs in region at start. 

            RidingClub.Clear();          
            foreach (var club in db.Clubs.Where(t => t.club_id >1 ))
            {
                RidingClub.Add(club);
            }
            cboRidingClub.ItemsSource = RidingClub;
           
        }


   


        public LoginRegister()
        {

            InitializeComponent();
            activeTab = "Logon";


        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

            bool validated;
            //obtain user details for passing in to the Dashboard screen
            string currentUser = tbxUsername.Text.Trim();
            string currentPassword = tbxPassword.Password.Trim();

            if (activeTab == "Logon")
            {
                validated = loginProcess.ValidateLoginData(currentUser, currentPassword);
                if (validated == true)
                {


                    // For existing logged on user
                    var query = (from u in db.Users
                                 join m in db.Members on u.airc_id equals m.airc_id
                                 where u.username == currentUser
                                 && u.userpassword == currentPassword
                                 select new
                                 {
                                     m.role,
                                     u.username,
                                     u.userpassword,
                                     u.airc_id,
                                     m.club_id
                                 });

                    foreach (var record in query)
                    {
                        MainDashboard maindashboard = new MainDashboard();
                        this.Hide();
                        close = true;
                        // set variables needed for the maindashboard. Mainly used for showing next avilable competition.
                        maindashboard.currentPassword = currentPassword;
                        maindashboard.tbxUsername.Text = record.username;
                        maindashboard.airc_id = record.airc_id;
                        maindashboard.member_role = record.role;
                        maindashboard.club_id = record.club_id;

                        maindashboard.ShowDialog();

                    }
                    this.Close();
                    // By Default if we reach here that means we have an invalid username and/or password entered
                    if (close == false)
                    {
                        MessageBox.Show($"Incorrect username or Password");

                    }
                    // We need to ensure that application is executed gracefully

                    else
                    {
                        if (close == false)
                        {
                            MessageBox.Show("Username or password not entered or too big");

                        }
                    }
                }
            }
            else if (activeTab == "Register")
            {
                airc_id = Convert.ToInt16(tbxAIRC_ID.Text.Trim());
                validated = registerProcess.ValidateRegistrationData(currentUser, currentPassword, airc_id, tbxFirstname.Text.Trim(), tbxLastName.Text.Trim(), club_id, DRgrade, SJgrade, XCgrade, tbxPhone.Text, tbxEmail.Text);


                string role = "M";  //Role is Member 
                string memberStatus = "N"; //Member status is "N" for new Membe

                // call Register user Method                
                RegisterUser(
                    airc_id,
                   currentUser,
                   currentPassword);
                // As user does not get id generated from an identity column, we are forced to save userdetails here. 
                // ideally we should use a db sequence value. 

                SaveRegistration();

                //RegisterMember  Method.   We should really have just passed in the mmeber Class. Will do so if time permits.
                RegisterMember(
                 airc_id,
                 club_id,
                 memberStatus,
                 role,
                 tbxFirstname.Text.Trim(),
                 tbxLastName.Text.Trim(),
                 DRgrade.Trim(),
                 SJgrade.Trim(),
                 XCgrade.Trim(),
                 tbxPhone.Text,
                 tbxEmail.Text);

                // Here we save the registration.
                SaveRegistration();
                //We force self registered users to log on with their new username and password.
                MessageBox.Show($"Your user {currentUser} has been saved. Please login again with your username and password");
                this.Close();
            }            
        }


        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            //everytime we select a tab we set e.Handled = true so that we don't get a growing list of unhandled tab selections. 
            if (sender is TabItem tab)
            {
                e.Handled = true;;
            }
        }

        private void Register_Selected(object sender, RoutedEventArgs e)
        {
            //set the visibility of items for when register is selected
            tbxEmail.Visibility = Visibility.Visible;
            tbxPhone.Visibility = Visibility.Visible;
            tbxFirstname.Visibility = Visibility.Visible;
            tbxLastName.Visibility = Visibility.Visible;
            tbxAIRC_ID.Visibility = Visibility.Visible;
            cboRidingClub.Visibility = Visibility.Visible;
            cboDR.Visibility = Visibility.Visible;
            cboSJ.Visibility = Visibility.Visible;
            cboXC.Visibility = Visibility.Visible;
            lblDR.Visibility = Visibility.Visible;
            lblSJ.Visibility = Visibility.Visible;
            lblXC.Visibility = Visibility.Visible;
            lblFirstName.Visibility = Visibility.Visible;
            lblLastName.Visibility = Visibility.Visible;
            lblAIRC_ID.Visibility = Visibility.Visible;
            lblRidingClub.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPhone.Visibility = Visibility.Visible;
            activeTab = "Register";
        }

        private void Logon_Selected(object sender, RoutedEventArgs e)
        {
            //set the visibility of items for when Logon is selected. 
            // Only username ansd password are visible.  should use stackpanel, so we can just hide show items easier.

            tbxFirstname.Visibility = Visibility.Collapsed;
            tbxLastName.Visibility = Visibility.Collapsed;
            tbxEmail.Visibility = Visibility.Collapsed;
            tbxPhone.Visibility = Visibility.Collapsed;
            tbxAIRC_ID.Visibility = Visibility.Collapsed;

            cboRidingClub.Visibility = Visibility.Collapsed;
            cboDR.Visibility = Visibility.Collapsed;
            cboSJ.Visibility = Visibility.Collapsed;
            cboXC.Visibility = Visibility.Collapsed;
            lblFirstName.Visibility = Visibility.Collapsed;
            lblLastName.Visibility = Visibility.Collapsed;

            lblAIRC_ID.Visibility = Visibility.Collapsed;
            lblDR.Visibility = Visibility.Collapsed;
            lblSJ.Visibility = Visibility.Collapsed;
            lblXC.Visibility = Visibility.Collapsed;
            lblRidingClub.Visibility = Visibility.Collapsed;
            lblEmail.Visibility = Visibility.Collapsed;
            lblPhone.Visibility = Visibility.Collapsed;

            activeTab = "Logon";
        }

        private void RegisterUser(int airc_id, string username, string password)
        {

            // add user to User entity
            User user = new User();
            user.airc_id = airc_id;
            user.username = username;
            //deliberately avoided using user and password so we do not clash with built ins.
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }

        private void RegisterMember(int airc_id, int club_id, string memberStatus, string role, string firstname, string lastname, string DR, string SJ, string XC, string phone, string email)
        {

            //add member details to member entity
            Member member = new Member();
            member.airc_id = airc_id;
            member.club_id = club_id;
            member.role = role;
            member.first_name = firstname;
            member.last_name = lastname;
            member.member_status = memberStatus;
            member.DR = DR;
            member.SJ = SJ;
            member.XC = XC;
            member.phone = phone;
            member.email = email;

            db.Entry(member).State = System.Data.Entity.EntityState.Added;


        }

        private void SaveRegistration()
        {
            //save Chanegs to DB;
            // should check here if register fails and delete user if it does. 
            db.SaveChanges();
        }

        private void CboDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Select value from comboBox for Dressage grade selected
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboDR.SelectedItem;
            DRgrade = item.Content.ToString();

        }

        private void CboSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Select value from comboBox for Show Jumping grade selected

            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboSJ.SelectedItem;
            SJgrade = item.Content.ToString();
        }



        private void CboXC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Select value from comboBox for Cross-Country grade selected

            var comboBoxItem = sender;
            ComboBoxItem item = (ComboBoxItem)cboXC.SelectedItem;
            XCgrade = item.Content.ToString();


        }



        private void CboRidingClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //select riding club id asociated with riding club selected.
            
            var comboBoxItem = (ComboBox)sender;         
            string club_id_str = cboRidingClub.SelectedValue.ToString();
            club_id = Convert.ToInt32( club_id_str);    
        }


    }

}
