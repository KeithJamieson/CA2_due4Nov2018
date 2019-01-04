using System;
using System.Windows;
using System.Windows.Controls;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for AddClub.xaml
    /// </summary>
    public partial class AddClub : Window
    {
        //set up access to database
        RELICEntities db = new RELICEntities();
        // declare variables for use with combobox grades 
        string DRgrade;
        string SJgrade;
        string XCgrade;
        
        int airc_id;  //riding club id 
        int club_id;  // id of newly created club 
        string member_status = "A"; //Club Secretaries automatically approved
        string role = "S"; //Role sceretary is assigned
        public AddClub()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // populate clubname from textbox
            string clubname = tbxClubName.Text.Trim();
            // convert value form textbox to a 32 bit inetger
            airc_id = Convert.ToInt32(tbxAIRC_ID.Text.Trim());

     
            RegisterClub(clubname); // create the club entry in the club table
            RegisterUser(airc_id,tbxUsername.Text.Trim(),tbxPassword.Password.Trim());    //create the logon information 
            // Create the memnber entry
            RegisterMember(
                airc_id,
                club_id,
                member_status, //Approved 
                role, // Secretary
                tbxFirstName.Text.Trim(),
                tbxLastName.Text.Trim(),
                DRgrade,
                SJgrade,
                XCgrade,
                tbxPhone.Text.Trim(),
                tbxEmail.Text.Trim()
                );

            int result = db.SaveChanges();
            if (result > 0)
            {
                MessageBox.Show($"Club  {clubname} has been added. Secretary is {tbxFirstName.Text.Trim()}  { tbxLastName.Text.Trim()} ");
            }
            else
            {
                // We should really delete the club record if there is an error. ignore it for now
                MessageBox.Show($"There was an error in Saving the Data. Please Contact Support ");
            }
            this.Close();
        }
        
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //abort opertaion and return to Dashboard
            this.Close();
        }

        private void RegisterClub(string clubname)
        {
            // create database entry for clubname
            Club newclub = new Club
            {
                clubname = clubname
            };
            db.Entry(newclub).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();  // in order to get the club_id returned from identity column, we need to save the changes to the database.  Using a sequence wouldprobably be better.
            club_id = db.Entry(newclub).Entity.club_id;  // obtain the club_id from the newly created club. 
          
        }

        private void DeleteClub(int club_id)
        {
            // was going to call this in the event of an error in registering club secretaryt after club has been created
            Club newclub = new Club();
            newclub.club_id = club_id;
            db.Entry(newclub).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
 

        }
        private void RegisterUser(int airc_id, string username, string password)
        {

            // method to create login details and link to member table.  Should have just passed in the User Class
            User user = new User();
             user.airc_id = airc_id;
            user.username = username;
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }

        private void RegisterMember(int airc_id, int club_id, string memberStatus, string role, string firstname, string lastname, string DR, string SJ, string XC, string phone, string email)
        {
            // create member record. Should have just passed in the Mmeber Class

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

        private void CboDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set variable to selected vaue from ComboBox
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboDR.SelectedItem;
            DRgrade = item.Content.ToString();

        }

        private void CboSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set variable to selected vaue from ComboBox
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboSJ.SelectedItem;
            SJgrade = item.Content.ToString();
        }



        private void CboXC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set variable to selected vaue from ComboBox
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboXC.SelectedItem;
            XCgrade = item.Content.ToString();
        }

    }
}
