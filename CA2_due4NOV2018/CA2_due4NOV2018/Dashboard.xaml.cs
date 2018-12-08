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
        private object ViewScheduledCompetitions;

        RELICEntities db = new RELICEntities();
        //Member member = new Member();
        public int airc_id;
        public string competitionSecretary;
        public string hostingClub;
        //string currentyear = DateTime.Now.ToString("yyyy");
        DateTime currentDate = DateTime.Now;
        int currentyear = DateTime.Now.Year;
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            User user = new User();
           Competition competition = new Competition();

            MainDashboard maindashboard = new MainDashboard();
            //  maindashboard.tbxCompetitionVenue.Text = competition.venue.ToString();
            //  maindashboard.tbxCompetitionDate.Text = competition.competition_date.ToString();
            //  maindashboard.tbxCompetitionName.Text = competition.venue.ToString();

         //   var query =  
            //maindashboard.tbxHostingClub.Text = competitionSecretary;
            //maindashboard.tbxCompetitionSecretary.Text = hostingClub;
            //foreach (var User in db.Users.Where(t => t.username == currentUser && t.username == currentPassword))

            //  skew.CalibrationDate.Date == db.Skew.Select(s => s.CalibrationDate.Date)
            //                          .Where(s => s <= date.Date)
            //                          .Max()

            //competition.competition_date == db.Competitions.Select(t => t.competition_date)
            //    .Where(t => t >= date.Date)
            //foreach (var record in db.Competitions.Where(t => t.competition_date.Date.Year.ToString() = currentyear.ToString() && t.competition_status == "S" && t.competition_date.Year == currentyear))
            //{
            //    tbxCompetitionDate.Text = record.competition_date.ToString();
            //    //tbxCompetitionName.Text = record.competition_name;
            //    tbxCompetitionVenue.Text = record.venue;
            //    //tbxHostingClub = record.club_id.ToString();
            //    //tbxCompetitionSecretary = record.airc_id.ToString();
            //};
          
                //Users.Where(t => t.Member == ;
            {
            //   // scheduledComps.Add(record);
            }
            //public int LoggedInUser;
            //public string username;
            //tbxUsername = LoginRegister.username;
            //airc_id=LoginRegister.LoggedInUser;
        }

 
        private void BtnChangeMyPassword_Click(object sender, RoutedEventArgs e)
        {

        }

 



        private void BtnModifyMyDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnModifyMemberDetails_Click(object sender, RoutedEventArgs e)
        {

        }



        private void BtnViewLeaderboard_Click(object sender, RoutedEventArgs e)
        {

        }
    

    private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnListNewMembers_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void btnScheduleCompetition_Click(object sender, RoutedEventArgs e)
        {
            ScheduleCompetition scheduledCompetition = new ScheduleCompetition();
            scheduledCompetition.ShowDialog();
           
        }

        private void btnViewScheduledCompetitions_Click(object sender, RoutedEventArgs e)
        {
            ViewScheduledCompetition viewScheduledCompetitions = new ViewScheduledCompetition();

            viewScheduledCompetitions.ShowDialog();
        }


        //private void CheckUserAccess(Db.User user)
        //{
        //    if (user.)
        //    {

        //    }
        //}
    }

  
}
