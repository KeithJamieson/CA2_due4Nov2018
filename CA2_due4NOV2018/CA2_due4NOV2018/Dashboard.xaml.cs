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
           
            refreshDashboard();
            //User user = new User();

            //var query = (from c in db.Competitions
            //             join m in db.Members on c.airc_id equals m.airc_id
            //             join rc in db.Clubs on c.club_id equals rc.club_id
            //             where c.competition_status == "S" && c.competition_date >= currentDate
            //                                && c.competition_date.Year == currentyear
            //                                orderby c.competition_date ascending
            //                                select new
            //                                {
            //                                    next_competition_date = c.competition_date,
            //                                    c.competition_id,
            //                                    c.competition_name,
            //                                    c.competition_type,
            //                                    competition_venue     = c.venue,
            //                                    Secretary = c.Member.first_name + " " + c.Member.last_name,
            //                                    hosting_club=rc.clubname,
            //                                    secretary_airc_id = m.airc_id,
            //                                    hosting_club_id = c.club_id
            //                                }).Take(1);

            //foreach (var record in query  )
            //{
            //    tbxCompetitionDate.Text = record.next_competition_date.ToString();
            //    tbxCompetitionName.Text = record.competition_name;
            //    tbxCompetitionVenue.Text = record.competition_venue;
            //    tbxCompetitionSecretary.Text = record.Secretary;
            //    tbxHostingClub.Text = record.hosting_club;
            //    competition_id = record.competition_id;
            //    competition_type = record.competition_type;
            //    secretary_airc_id = record.secretary_airc_id;
            //    hosting_club_id = record.hosting_club_id;
            //}


            if (member_role == "M") 
            {
                btnAddClub.Visibility = Visibility.Collapsed;
                btnAddCompetition.Visibility = Visibility.Collapsed;
                btnListNewMembers.Visibility = Visibility.Collapsed;
                btnModifyMemberDetails.Visibility = Visibility.Collapsed;
                btnModifyMyDetails.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
                btnViewLeaderboard.Visibility = Visibility.Visible;
                btnViewReports.Visibility = Visibility.Visible;
                btnViewScheduledCompetitions.Visibility = Visibility.Visible;
                btnOpenCompetition.Visibility = Visibility.Collapsed;
            }

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

            if (tbxCompetitionDate.Text.ToString() == currentDate.ToString() )
            {
                if (secretary_airc_id == airc_id)
                {
                    btnOpenCompetition.Visibility = Visibility.Visible;
                }
            }

           // btnOpenCompetition.Visibility = Visibility.Visible;  //temporary
        }

 
        private void BtnChangeMyPassword_Click(object sender, RoutedEventArgs e)
        {

            ChangePassword changepassword = new ChangePassword();
            changepassword.username = tbxUsername.Text.Trim();
            changepassword.currentPassword = currentPassword;
            changepassword.ShowDialog();
            //if password is changed on password screen we need it to be reflected on dashboard screen
            currentPassword = changepassword.currentPassword;
            
        }

 


        private void BtnModifyMyDetails_Click(object sender, RoutedEventArgs e)
        {
            ModifyUserDetails modifyUserDetails = new ModifyUserDetails();
            modifyUserDetails.Username = tbxUsername.Text;
            modifyUserDetails.ShowDialog();
        }

        private void BtnModifyMemberDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality has not been implemented");
            ModifyUserDetails modifymydetails = new ModifyUserDetails();
   //         ModifyUserdetails.Username = tbxUsername.Text;
   //         ModifyUserdetails.ShowDialog();
        }



        private void BtnViewLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            
            ShowLeaderBoard leaderboard = new ShowLeaderBoard();
            leaderboard.ShowDialog();
        }
    

    private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {
            RunCompetition runcompetition = new RunCompetition();
            runcompetition.ShowDialog();
        }

        private void BtnListNewMembers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality has not been implemented");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
            

        private void BtnAddClub_Click(object sender, RoutedEventArgs e)
        {
            AddClub addclub = new AddClub();                        
            addclub.ShowDialog();
        }

             
        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            if ((airc_id == secretary_airc_id))
            {

                RunCompetition runcompetition = new RunCompetition();
                runcompetition.tbxCompetitionName.Text = tbxCompetitionName.Text;
                runcompetition.tbxCompetitionDate.Text = tbxCompetitionDate.Text;
                runcompetition.competition_id = competition_id;

                Competition  competition = new Competition();
               foreach (var thiscompetition in db.Competitions.Where( t => t.competition_id == competition_id))
                    {
                       thiscompetition.competition_status = "O";
                    }

                    int SaveSuccess = db.SaveChanges();

                if (SaveSuccess == 1)
                {
                    runcompetition.ShowDialog();
                    refreshDashboard();
                }
                else
                {

                        MessageBox.Show("Unable to Open Competition ");
                }
               // db.SaveChanges();

            }
            else
                MessageBox.Show("You must be competition Secretary to open competition");
        }

        private void btnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
            AddCompetition addCompetition = new AddCompetition();
            addCompetition.hosting_club_id = club_id;
            addCompetition.ShowDialog();
            refreshDashboard();
        }

        private void BtnViewScheduledCompetitions_Click(object sender, RoutedEventArgs e)
        {
            ViewScheduledCompetition viewCompetitions = new ViewScheduledCompetition();
            viewCompetitions.hosting_club_id = hosting_club_id;
            viewCompetitions.club_id = club_id;
            viewCompetitions.ShowDialog();

        }

        private void refreshDashboard()
        {
            User user = new User();

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

        }

    }

  
}
