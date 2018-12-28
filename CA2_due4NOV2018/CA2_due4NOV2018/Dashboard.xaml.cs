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
using System.Xml.Linq;
namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainDashboard : Window
    {
       

        RELICEntities db = new RELICEntities();
        //Member member = new Member();
        public int club_id;  //Club_id of logged in member
        public int hosting_club_id; //Club_id of club hosting next competition
        public int airc_id;
        int secretary_airc_id;
        public string competitionSecretary;
        public string hostingClub;   
        DateTime currentDate = DateTime.Today;
        int currentyear = DateTime.Now.Year;
        public int competition_id;
        public string competition_type;
        public string member_role;
        public string member_status;
        public string currentPassword;
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // Refresh Dashboard on loading 
            RefreshDashboard();

            // Make buttons visible or collapsed depending on member role

            // Ordinary member
            if (member_role == "M") 
            {
                btnAddClub.Visibility = Visibility.Collapsed;
                btnAddCompetition.Visibility = Visibility.Collapsed;
                btnListNewMembers.Visibility = Visibility.Collapsed;
                btnModifyMemberDetails.Visibility = Visibility.Collapsed;
                btnModifyMyDetails.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
                btnViewLeaderboard.Visibility = Visibility.Visible;
                btnViewReports.Visibility = Visibility.Collapsed;
                btnViewScheduledCompetitions.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
            }

            // Club Secretary
            if (member_role == "S")
            {
                btnAddClub.Visibility = Visibility.Collapsed;
                btnAddCompetition.Visibility = Visibility.Visible;
                btnListNewMembers.Visibility = Visibility.Visible;
                btnModifyMemberDetails.Visibility = Visibility.Visible;
                btnModifyMyDetails.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
                btnViewLeaderboard.Visibility = Visibility.Visible;
                btnViewReports.Visibility = Visibility.Visible;
                btnViewScheduledCompetitions.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
            }

            // Administrator
            if (member_role == "A")
            {
                btnAddClub.Visibility = Visibility.Visible;
                btnAddCompetition.Visibility = Visibility.Visible;
                btnListNewMembers.Visibility = Visibility.Visible;
                btnModifyMemberDetails.Visibility = Visibility.Visible;
                btnModifyMyDetails.Visibility = Visibility.Visible;
                btnViewLeaderboard.Visibility = Visibility.Visible;
                btnViewReports.Visibility = Visibility.Visible;
                btnViewScheduledCompetitions.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
            }

            // if logged in user is competition secretary and competition scheduled date is today, then mkae the OpenCompetition button visible. 
            if (tbxCompetitionDate.Text.ToString() == currentDate.ToString() )
            {
                if (secretary_airc_id == airc_id)
                {
                    btnOpenCompetition.Visibility = Visibility.Visible;
                }
            }

        }

 
        private void BtnChangeMyPassword_Click(object sender, RoutedEventArgs e)
        {
            // call ChangePassword Window
            ChangePassword changepassword = new ChangePassword();
            changepassword.username = tbxUsername.Text.Trim();
            changepassword.currentPassword = currentPassword;
            changepassword.ShowDialog();
            //if password is changed on password screen we need it to be reflected on dashboard screen
            currentPassword = changepassword.currentPassword;
            
        }

 


        private void BtnModifyMyDetails_Click(object sender, RoutedEventArgs e)
        {
            // Change my details 
            ModifyUserDetails modifyUserDetails = new ModifyUserDetails();
            modifyUserDetails.Username = tbxUsername.Text;
            modifyUserDetails.ShowDialog();
        }

        private void BtnModifyMemberDetails_Click(object sender, RoutedEventArgs e)
        {
            // This is same screen as ModifyUserDetails except the User to be modified would be chosen by club secretary
            MessageBox.Show("Functionality has not been implemented");
            ModifyUserDetails modifymydetails = new ModifyUserDetails();
   //         ModifyUserdetails.Username = tbxUsername.Text;
   //         ModifyUserdetails.ShowDialog();
        }



        private void BtnViewLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            // Display leaderboard
            ShowLeaderBoard leaderboard = new ShowLeaderBoard();
            leaderboard.ShowDialog();
        }
    

    private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {
            // view the reports screen
          Reports reports = new Reports();
            reports.ShowDialog();           
        }

        private void BtnListNewMembers_Click(object sender, RoutedEventArgs e)
        {
            // List all members in users club  with a status of N(ew)
            MessageBox.Show("Functionality has not been implemented");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            // exit dashboard and exit application
            this.Close();

        }
   
            

        private void BtnAddClub_Click(object sender, RoutedEventArgs e)
        {
            // Add a new club (and club secretary)
            AddClub addclub = new AddClub();                        
            addclub.ShowDialog();
        }

             
        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            // validate we are club secretary
            if ((airc_id == secretary_airc_id))
            {
                // call runcompetition screen
                RunCompetition runcompetition = new RunCompetition();
                // set variables to be used in runCompetitionscreen
                runcompetition.tbxCompetitionName.Text = tbxCompetitionName.Text;
                runcompetition.tbxCompetitionDate.Text = tbxCompetitionDate.Text;
                runcompetition.competition_type = competition_type;
                runcompetition.competition_id = competition_id;

                // Change Competition Status from S(cheduled) to O(pen). 
                Competition competition = new Competition();
                
                foreach (var thiscompetition in db.Competitions.Where(t => t.competition_id == competition_id))
                {
                    thiscompetition.competition_status = "O";
                }

                int SaveSuccess = db.SaveChanges();

                if (SaveSuccess == 1)
                {
                    // call runcompetition screen
                    runcompetition.ShowDialog(); 
                    // after competition finished refresh dashboard. 
                    RefreshDashboard(); 
                }
                else
                {

                    MessageBox.Show("Unable to Open Competition ");
                }


            }
            else

            {
                MessageBox.Show("You must be competition Secretary to open competition");
            }
        }

        private void BtnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
            // schedule a new competition
            AddCompetition addCompetition = new AddCompetition();
            addCompetition.hosting_club_id = club_id;
            addCompetition.ShowDialog();
            RefreshDashboard();
        }

        private void BtnViewScheduledCompetitions_Click(object sender, RoutedEventArgs e)
        {
            // view a list of competitions available in current year.
            ViewScheduledCompetition viewCompetitions = new ViewScheduledCompetition();
            viewCompetitions.hosting_club_id = hosting_club_id;
            viewCompetitions.club_id = club_id;
            viewCompetitions.ShowDialog();

        }

        public void RefreshDashboard()
        {
            // refresh dashboard
            User user = new User();

            // we get the next available competition in the current year.
            var query = (from c in db.Competitions
                         join m in db.Members on c.airc_id equals m.airc_id
                         join rc in db.Clubs on c.club_id equals rc.club_id
                         where c.competition_status == "S" && c.competition_date >= currentDate
                                            && c.competition_date.Year == currentyear
                         orderby c.competition_date ascending
                         select new
                         {
                             next_competition_date = c.competition_date,
                             c.competition_id,
                             c.competition_name,
                             c.competition_type,
                             competition_venue = c.venue,
                             Secretary = c.Member.first_name + " " + c.Member.last_name,
                             hosting_club = rc.clubname,
                             secretary_airc_id = m.airc_id,
                             hosting_club_id = c.club_id
                         }).Take(1);

            // Here we populate our competition details on the dashboard screen.
            foreach (var record in query)
            {
                tbxCompetitionDate.Text = record.next_competition_date.ToString();
                tbxCompetitionName.Text = record.competition_name;
                tbxCompetitionVenue.Text = record.competition_venue;
                tbxCompetitionSecretary.Text = record.Secretary;
                tbxHostingClub.Text = record.hosting_club;
                competition_id = record.competition_id;
                competition_type = record.competition_type;
                secretary_airc_id = record.secretary_airc_id;
                hosting_club_id = record.hosting_club_id;
            }

            if (tbxCompetitionDate.Text.ToString() == currentDate.ToString())
            {
                if (secretary_airc_id == airc_id)
                {
                    btnOpenCompetition.Visibility = Visibility.Visible;
                }
            }
        }

    }

  
}
