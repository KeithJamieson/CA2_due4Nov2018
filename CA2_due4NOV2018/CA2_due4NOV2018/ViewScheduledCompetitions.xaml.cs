using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
//using System.Windows.Controls;
//using System;
using System.Windows.Data;

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

       
        //List<ViewScheduledCompetition> lstScheduledCompetitions = new List<ViewScheduledCompetition>();
        List<Competition> lstScheduledCompetitions = new List<Competition>();

        public ViewScheduledCompetition()
        {
            InitializeComponent();
        }

       
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void BtnAmend_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
            AddCompetition addCompetition = new AddCompetition();
            addCompetition.ShowDialog();
            RefreshCompetitionList();
        }

        private void RefreshCompetitionList()
        {
            lstScheduledCompetitions.Clear();
            
            foreach (var record in db.Competitions.Where(t => t.competition_status == "S" && t.competition_date >= currentDate && t.competition_date.Year == currentyear ))
            {
               
                lstScheduledCompetitions.Add(record);

            }
            lstViewCompetitionSchedule.ItemsSource = lstScheduledCompetitions;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstViewCompetitionSchedule.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("competition_date", ListSortDirection.Ascending));

            
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbxScheduleYear.Text = $"Scheduled competitions for {currentyear}";
            RefreshCompetitionList();

        }



        private void btnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            // change competition status from "S" to "O"
            // only allowed if I am competition secretary and today = competition date            
            // Competition competition = new Competition();
            // competition.ShowDialog();
            Competitio window = new Competitio();
            window.ShowDialog();
        }

    }
}
