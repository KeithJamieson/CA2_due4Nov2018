using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RunCompetition : Window
    {

        public RunCompetition()
        {
            InitializeComponent();
        }

        RELICEntities db = new RELICEntities();
        List<Entry> CompetitionEntries = new List<Entry>();
        System.DateTime currentDate = System.DateTime.Today;
        int currentyear = System.DateTime.Now.Year;

        List<Competition> lstScheduledCompetitions = new List<Competition>();

        public int competition_id;
        string Ridergrade;
        string activeTab;

        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }


        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AP";
            RefreshList(Ridergrade);
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "I";
            RefreshList(Ridergrade);
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AI";
            RefreshList(Ridergrade);
        }

        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "O";
            RefreshList(Ridergrade);
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AO";
            RefreshList(Ridergrade);
        }





        private void BtnCloseCompetition_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnUpdateLeaderBoard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {
            stkAssignFinishingPosition.Visibility = Visibility.Visible;
        }

        private void BtnAddRiderEntry_Click(object sender, RoutedEventArgs e)
        {
            AddRider addRider = new AddRider();
            addRider.competition_id = competition_id;           
            addRider.ShowDialog();
            lstRiders.Items.Refresh();

        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void RefreshList(string Ridergrade)
        {

            CompetitionEntries.Clear();
            
            foreach (var record in db.Entries.Where(t => t.competition_id == competition_id && t.grade == Ridergrade))
            {
                CompetitionEntries.Add(record);
            }
            lstRiders.ItemsSource = CompetitionEntries;
            lstRiders.Items.Refresh();

                     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (var competition in db.Competitions.Where (t => t.competition_id == competition_id))
            {
                competition.competition_status = "O";
            }

            stkAssignFinishingPosition.Visibility = Visibility.Collapsed;

            //    = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();

            Ridergrade = "P";
            RefreshList(Ridergrade);

        }



        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TabItem tab)
            {                
                e.Handled = true;
            }
        }

  

        private void LstRiders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //NB Ridergrade holds current grade
                Entry SelectedRider = CompetitionEntries.ElementAt(lstRiders.SelectedIndex);
                if (SelectedRider.airc_id > 0)   // ensure we have a record
                {
                    tbxAIRC_ID.Text = SelectedRider.airc_id.ToString();
                    tbxLastName.Text = SelectedRider.Lastname.Trim();
                    tbxFirstName.Text = SelectedRider.Firstname.Trim();
                    tbxHorse.Text = SelectedRider.Horse.Trim();
                    tbxClub.Text = SelectedRider.clubname.Trim();
                    tbxPlace.Text = SelectedRider.points.ToString();

                }
            }
            catch (System.Exception)
            {
               
                //throw;
            }
            //Entry CompetitionEntries = new List<Entry>();

        
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            Entry entry = new Entry();
            entry.airc_id = Convert.ToInt32(tbxAIRC_ID.Text);
            entry.clubname = tbxClub.Text;
            entry.Firstname = tbxFirstName.Text;
            entry.Lastname = tbxLastName.Text;
            entry.Horse = tbxHorse.Text;
            entry.competition_id = competition_id;
            entry.points = Convert.ToInt32(tbxPlace.Text);
            entry.grade = Ridergrade;
            SaveEntry(entry);            
            
        }

        public void SaveEntry(Entry entry)
        {
            
            db.Entry(entry).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            RefreshList(Ridergrade);
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkAssignFinishingPosition.Visibility = Visibility.Collapsed;
            
        }
    }
}
