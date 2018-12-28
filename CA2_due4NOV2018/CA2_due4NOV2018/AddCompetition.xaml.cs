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

        RELICEntities db = new RELICEntities();

        List<Member> memberslist = new List<Member>();
        public int hosting_club_id;
        public int club_id;
        int airc_id;
        //      int competitionSecretary;

        public AddCompetition()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
         
        }




        string CompetitionDiscipline = "";

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {   
            Competition competition = new Competition();

            competition.venue = tbxCompetitionVenue.Text.Trim();
            competition.competition_name = tbxCompetitionName.Text;
            competition.competition_date =Convert.ToDateTime(dpkCompetitionDate.ToString());
            competition.competition_type = CompetitionDiscipline;
            competition.competition_status = "S";
            competition.club_id = hosting_club_id;
            competition.airc_id = airc_id;
            ScheduleCompetitionSave(competition);
            MessageBox.Show("Competition has been successfully scheduled");
            this.Close();
        }

        private static Competition GetCompetition(Competition competition)
        {
            return competition;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshList()
        {
            CboCompetitionSecretary.ItemsSource = "";
            memberslist.Clear();
            foreach (var member in db.Members.Where(t => t.club_id == hosting_club_id))
            {
                memberslist.Add(member);
            }
            CboCompetitionSecretary.ItemsSource = memberslist;
        }
        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CboCompetitionType_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            var comboBoxItem = sender;
            ComboBoxItem item = (ComboBoxItem)cboCompetitionType.SelectedItem;
            CompetitionDiscipline = item.Content.ToString();
        }

        private void CboCompetitionSecretary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            string airc_id_str = CboCompetitionSecretary.SelectedValue.ToString();             
            airc_id = Convert.ToInt32(airc_id_str);
        }

        private void ScheduleCompetitionSave(Competition competition)
        {
        
            db.Entry(competition).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
    }
}
