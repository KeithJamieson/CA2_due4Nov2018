using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for SchdeuleCompetition.xaml
    /// </summary>
    public partial class AddCompetition : Window
    {
        // set up DB access
        RELICEntities db = new RELICEntities();
        // Create list for obtaining members who can act as competition secretary
        List<Member> memberslist = new List<Member>();
        public int hosting_club_id;  // Id of riding club which is holding the competition
        public int club_id;   // should be same as hosting_club_id.  
        int airc_id;  riding club id of competitin secretary
        //      int competitionSecretary;

        public AddCompetition()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  set up details when we enter screen 
            RefreshList();
         
        }




        string CompetitionDiscipline = "";  // default to null. It must be set

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {   
            // Save Scheduled competition
            Competition competition = new Competition();

            competition.venue = tbxCompetitionVenue.Text.Trim();
            competition.competition_name = tbxCompetitionName.Text;
            competition.competition_date =Convert.ToDateTime(dpkCompetitionDate.ToString());  // needs to be formatted to lose midnight element
            competition.competition_type = CompetitionDiscipline;
            competition.competition_status = "S";  // Newly created competitions are given status of S (Scheduled)
            competition.club_id = hosting_club_id;   //id of  club holding competition
            competition.airc_id = airc_id;           // riding club id of competitioj secretary
            ScheduleCompetitionSave(competition);
            MessageBox.Show("Competition has been successfully scheduled");

            this.Close();   // Close Window
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Cancel Scheduling of Competition.
        }

        private void RefreshList()
        {
            // used whenvever screen is entered
            CboCompetitionSecretary.ItemsSource = "";  // Clear ComboBox
            memberslist.Clear();
            foreach (var member in db.Members.Where(t => t.club_id == hosting_club_id))
            {
                memberslist.Add(member);
            }
            CboCompetitionSecretary.ItemsSource = memberslist;
        }
        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            // not functional
        }

        private void CboCompetitionType_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            // ComboBox used to select competition type;
            var comboBoxItem = sender;
            ComboBoxItem item = (ComboBoxItem)cboCompetitionType.SelectedItem;
            CompetitionDiscipline = item.Content.ToString();
        }

        private void CboCompetitionSecretary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ComboBox used to select CompetitionSecretary type;
            var comboBoxItem = (ComboBox)sender;
            string airc_id_str = CboCompetitionSecretary.SelectedValue.ToString();             
            airc_id = Convert.ToInt32(airc_id_str);
        }

        private void ScheduleCompetitionSave(Competition competition)
        {
          // Save scheduled Competition
            db.Entry(competition).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            
        }
    }
}
