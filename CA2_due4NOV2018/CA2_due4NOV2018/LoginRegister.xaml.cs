using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for LoginRegister.xaml
    /// </summary>
    public partial class LoginRegister : Window
    {

        //string conneectionString = "metadata=res://*/RelicModel.csdl|res://*/RelicModel.ssdl|res://*/RelicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=localhost;initial catalog=RELIC;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'";
        RELICEntities db = new RELICEntities();
        //List<user> userlist = new List<user>();

        //List<Grade> gradelist = new List<Grade>();
       
        
        public string activeTab;

        //User user = new User();
        public LoginRegister()
        {

            InitializeComponent();
            activeTab = "Logon";
        }

        
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //tbxUsername.Text=$"Active tab is {activeTab}";
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;

            if (activeTab == "Logon")
            {
               
                foreach (var User in db.Users.Where( t => t.username == currentUser && t.username == currentPassword ))
                {

                    MainDashboard maindashboard = new MainDashboard();
                    this.Hide();
                    maindashboard.ShowDialog();

                }
                // By Default if we reach here that means we have an invalid username and/or password entered
                MessageBox.Show($"Incorrect username or Password");
            }
            else if (activeTab == "Register")
            {
                // call Register user Class
                int club_id = 0;
                string role = "M";  //Role is Member 
                string memberStatus = "N"; //Member status is "N" for new Member
                int airc_id = Convert.ToInt16(tbxAIRC_ID.Text);
                              


                RegisterUser(
                    airc_id,
                   currentUser,
                   currentPassword);

                //SaveRegistration();

                RegisterMember(
                 airc_id,
                 club_id,
                 memberStatus,
                 role,
                 tbxFirstname.Text,
                 tbxLastName.Text,
                 "AP", //cboDR.Text,
                 "AP", //cboSJ.Text,
                 "AP", //cboXC.Text,
                 tbxPhone.Text,
                 tbxEmail.Text);

                SaveRegistration();

                MessageBox.Show($"User {currentUser} has been saved. Please login again with your username and password");
                this.Close();
            }

        }


        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem tab)
            {
                e.Handled = true;
                //MessageBox.Show("Tab selected");
                // this tab is selected!
                // string tabname = (string) tab.Content;
                //MessageBox.Show("Tab {tabname} is selected");
            }
        }

        private void Register_Selected(object sender, RoutedEventArgs e)
        {
            tbxEmail.Visibility         = Visibility.Visible;
            tbxPhone.Visibility         = Visibility.Visible;
           tbxFirstname.Visibility = Visibility.Visible;
           tbxLastName.Visibility = Visibility.Visible;
            tbxAIRC_ID.Visibility = Visibility.Visible; 
            cboRidingClub.Visibility    = Visibility.Visible;
            cboDR.Visibility            = Visibility.Visible;
            cboSJ.Visibility            = Visibility.Visible;
            cboXC.Visibility            = Visibility.Visible;
            lblDR.Visibility = Visibility.Visible;
            lblSJ.Visibility = Visibility.Visible;
            lblXC.Visibility = Visibility.Visible;
            lblFirstName.Visibility = Visibility.Visible;
            lblLastName.Visibility = Visibility.Visible;
            lblAIRC_ID.Visibility = Visibility.Visible;
            lblRidingClub.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPhone.Visibility = Visibility.Visible;
            // e.Handled = true;
            activeTab = "Register";
            //MessageBox.Show("Register Tab selected");
        }

        private void Logon_Selected(object sender, RoutedEventArgs e)
        {
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
            //  e.Handled = true;

            //MessageBox.Show("Logon Tab selected");
        }

        private void RegisterUser(int airc_id, string username, string password)
        {
          

            User user = new User();
            user.airc_id=airc_id;            
            user.username = username;
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }   

        private void RegisterMember(int airc_id,int club_id,  string memberStatus, string role, string firstname, string lastname, string DR, string SJ, string XC, string phone, string email )
        {

           Member member = new Member();
           member.airc_id = airc_id;
           member.club_id = club_id;
           member.role = role;
           member.first_name = firstname;
           member.last_name = lastname;
            member.member_status = memberStatus;
            member.DR =  DR;
            member.SJ =  SJ;
            member.XC =  XC;
           member.phone = phone;
           member.email = email;

            db.Entry(member).State = System.Data.Entity.EntityState.Added;
  
            
        }

        private void SaveRegistration()
        {           
            db.SaveChanges();
        }

    }

}
