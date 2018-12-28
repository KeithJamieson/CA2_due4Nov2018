using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for ScheduledCompetitions.xaml
    /// </summary>
    
    public partial class ViewScheduledCompetition : Window
    {
        RELICEntities db = new RELICEntities();
        System.DateTime currentDate = System.DateTime.Today;
        int currentyear = System.DateTime.Now.Year;
        List<Member> memberslist = new List<Member>();
        public int hosting_club_id;
        public int club_id;
        List<Competition> lstScheduledCompetitions = new List<Competition>();
        public ViewScheduledCompetition()
        {
            InitializeComponent();
        }

       
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            // return to dashboard
            this.Close();
            
        }

        private void BtnAmend_Click(object sender, RoutedEventArgs e)
        {
            stkViewCompetitions.Visibility = Visibility.Visible;
        }

        private void BtnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality not implemented from this screen");
        }
        private void BtnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
            // allows us to call the addCompetition Screen
            AddCompetition addCompetition = new AddCompetition();
            addCompetition.hosting_club_id = club_id;
            addCompetition.club_id =  club_id;
            addCompetition.ShowDialog();
            RefreshCompetitionList();
        }

        private void RefreshCompetitionList()
        {
            // called after we add a new competition.  It will be fitted in the schedule in  order of next upcomimg competition
            lstScheduledCompetitions.Clear();
            
            // We only view competitions for the current year
            foreach (var record in db.Competitions.Where(t => t.competition_status == "S" && t.competition_date >= currentDate && t.competition_date.Year == currentyear ))
            {
               
                lstScheduledCompetitions.Add(record);

            }
            lstViewCompetitionSchedule.ItemsSource = lstScheduledCompetitions;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstViewCompetitionSchedule.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("competition_date", ListSortDirection.Ascending));  // perform date sort 
            lstViewCompetitionSchedule.Items.Refresh();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbxScheduleYear.Text = $"Scheduled competitions for {currentyear}";
            RefreshCompetitionList();

        }



        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality not implemented from this screen");
            // change competition status from "S" to "O"

        }




        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality not implemented from this screen");
            
            //Thsi was going to be sued to save changes including comp secretary changes. Just out of time :(

           // Competition competition = new Competition();
           // //competition.club_id = Convert.ToInt32(tbxAIRC_ID.Text);
           // competition.competition_date = Convert.ToDateTime(tbxCompetitionDate.Text);
           // competition.competition_type = tbxCompetitionType.Text;
           // competition.venue = tbxCompetitionVenue.Text;
           // //competition.club_id = tbxClub.Text
           //// SaveEntry(entry);
           // RefreshCompetitionList();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkViewCompetitions.Visibility = Visibility.Collapsed;
        }

        private void CboCompetitionSecretary_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Display members of club hosting competition
            memberslist.Clear();            
            foreach (var member in db.Members.Where(t => t.club_id == hosting_club_id))
            {
                memberslist.Add(member);
            }
            cboCompetitionSecretary.ItemsSource = memberslist;
            //cboCompetitionSecretary.Text = competition.Member.airc_id;


        }

        private void LstViewCompetitionSchedule_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Populates stack panel with details form selected competition
            Competition competition = lstScheduledCompetitions.ElementAt(lstViewCompetitionSchedule.SelectedIndex);
            tbxCompetitionDate.Text = competition.competition_date.ToShortDateString();
            tbxCompetitionName.Text = competition.competition_name;
            tbxCompetitionType.Text = competition.competition_type;
            tbxCompetitionVenue.Text = competition.venue;
            memberslist.Clear();
            foreach (var member in db.Members.Where(t => t.club_id == hosting_club_id))
            {
                memberslist.Add(member);
            }
            cboCompetitionSecretary.ItemsSource = memberslist;
            
        }
    }
}
