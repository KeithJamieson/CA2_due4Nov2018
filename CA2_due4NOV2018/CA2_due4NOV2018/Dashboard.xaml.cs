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
        public int airc_id;
        public string competitionSecretary;
        public string hostingClub;
        //string currentyear = DateTime.Now.ToString("yyyy");
        DateTime currentDate = DateTime.Now;
        int currentyear = DateTime.Now.Year;
        public int competition_id;
        public string competition_type;
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
                                                competition_venue     = c.venue,
                                                Secretary = c.Member.first_name + " " + c.Member.last_name,
                                                hosting_club=c.Club.clubname
                                            }).Take(1);

            foreach (var record in query  )
            {
                tbxCompetitionDate.Text = record.next_competition_date.ToShortDateString();
                tbxCompetitionName.Text = record.competition_name;
                tbxCompetitionVenue.Text = record.competition_venue;
                tbxCompetitionSecretary.Text = record.Secretary;
                tbxHostingClub.Text = record.hosting_club;
                competition_id = record.competition_id;
                competition_type = record.competition_type;


                
            }
            MainDashboard maindashboard = new MainDashboard();

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
            //Competition competition = new Competition();
            //maindashboard.tbxUsername.Text = currentUser;
            //maindashboard.airc_id = User.airc_id;
            //maindashboard.ShowDialog();
        
            Competitio window = new Competitio();

            window.tbxCompetitionName.Text = tbxCompetitionName.Text;
            window.tbxCompetitionDate.Text = tbxCompetitionDate.Text;
            window.competition_id = competition_id;
            window.ShowDialog();
            //competition.ShowDialog();
        }

        private void btnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
            AddCompetition addCompetition = new AddCompetition();
            addCompetition.ShowDialog();

        }

        private void BtnViewScheduledCompetitions_Click(object sender, RoutedEventArgs e)
        {
            ViewScheduledCompetition viewCompetitions = new ViewScheduledCompetition();

            viewCompetitions.ShowDialog();
        }



    }

  
}
