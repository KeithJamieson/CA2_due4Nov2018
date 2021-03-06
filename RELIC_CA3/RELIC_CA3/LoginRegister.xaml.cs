﻿using RELICDBLibrary;
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

namespace RELIC_CA3
{
    /// <summary>
    /// Interaction logic for LoginRegister.xaml
    /// </summary>
    public partial class LoginRegister : Window
    {
        string DRgrade;
        string SJgrade;
        string XCgrade;
        int airc_id = 0;
        int club_id = 0;
        string member_role;
        //string conneectionString = "metadata=res://*/RelicModel.csdl|res://*/RelicModel.ssdl|res://*/RelicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=localhost;initial catalog=RELIC;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'";
        RELICEntities db = new RELICEntities();


        //  List<Grade> GradeList = new List<Grade>();
        List<Club> RidingClub = new List<Club>();

        Boolean close = false;
        public string activeTab;

        public LoginRegister()
        {

            InitializeComponent();
            activeTab = "Logon";


        }

        private void CboRidingClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            string club_id_str = cboRidingClub.SelectedValue.ToString();
            club_id = Convert.ToInt32(club_id_str);
        }

        private void CboDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboDR.SelectedItem;
            DRgrade = item.Content.ToString();
        }

        private void CboSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboSJ.SelectedItem;
            SJgrade = item.Content.ToString();
        }

        private void CboXC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = sender;
            ComboBoxItem item = (ComboBoxItem)cboXC.SelectedItem;
            XCgrade = item.Content.ToString();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;

            if (activeTab == "Logon")
            {
                //var query = (from c in db.Competitions
                //             join m in db.Members on c.airc_id equals m.airc_id
                //             join rc in db.Clubs on c.club_id equals rc.club_id
                //             where c.competition_status == "S" && c.competition_date >= currentDate
                //             && c.competition_date.Year == currentyear
                //             orderby c.competition_date ascending
                //             select new
                //             {
                //                 next_competition_date = c.competition_date,
                //                 c.competition_id,
                //                 c.competition_name,
                //                 c.competition_type,
                //                 competition_venue = c.venue,
                //                 Secretary = c.Member.first_name + " " + c.Member.last_name,
                //                 hosting_club = c.Club.clubname
                //             }).Take(1);

                var query = (from u in db.Users
                             join m in db.Members on u.airc_id equals m.airc_id
                             where u.username == tbxUsername.Text
                             && u.userpassword == tbxPassword.Password
                             select new
                             {
                                 m.role,
                                 u.username,
                                 u.userpassword,
                                 u.airc_id
                             });

                foreach (var record in query)
                {
                    //LoggedInUser = User.airc_id;


                    mainDashboard maindashboard = new mainDashboard();
                    this.Hide();
                    close = true;
                    maindashboard.tbxUsername.Text = record.username;
                    maindashboard.airc_id = record.airc_id;
                    maindashboard.member_role = record.role;
                    maindashboard.ShowDialog();

                }
                // By Default if we reach here that means we have an invalid username and/or password entered
                if (close == false)
                {
                    MessageBox.Show($"Incorrect username or Password");

                }
            }
            else if (activeTab == "Register")
            {
                // call Register user Class                
                airc_id = Convert.ToInt16(tbxAIRC_ID.Text.Trim());
                string role = "M";  //Role is Member 
                string memberStatus = "N"; //Member status is "N" for new Membe


                RegisterUser(
                    airc_id,
                   currentUser,
                   currentPassword);

                SaveRegistration();

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

                //     SaveRegistration();

                MessageBox.Show($"Your user {currentUser} has been saved. Please login again with your username and password");
                this.Close();
            }

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            this.Close();
            
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
        }

        private void Register_Selected(object sender, RoutedEventArgs e)
        {
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
            // e.Handled = true;
            activeTab = "Register";
            //MessageBox.Show("Register Tab selected");
        }
        private bool ValidateRegistrationData()
        {
            bool validated = true;
            if (tbxUsername.Text.Length == 0 || tbxUsername.Text.Length > 10)
            {
                validated = false;
            }

            if (tbxPassword.Password.Length == 0 || tbxPassword.Password.Length > 10)
            {
                validated = false;
            }

            return validated;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            RidingClub.Clear();
            foreach (var User in db.Users.Where(t => t.username == currentUser && t.username == currentPassword))

                foreach (var club in db.Clubs.Where(t => t.club_id > 1))
            {
                RidingClub.Add(club);

            }
            cboRidingClub.ItemsSource = RidingClub;
        }

        private void RegisterUser(int airc_id, string username, string password)
        {

            User user = new User();
            user.airc_id = airc_id;
            user.username = username;
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }
        private void RegisterMember(int airc_id, int club_id, string memberStatus, string role, string firstname, string lastname, string DR, string SJ, string XC, string phone, string email)
        {

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
            db.SaveChanges();
        }


    }
}
