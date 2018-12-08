using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for ScheduledCompetitions.xaml
    /// </summary>
    
    public partial class ViewScheduledCompetition : Window
    {
        RELICEntities db = new RELICEntities();
        string currentyear = System.DateTime.Now.Year.ToString();
        //
        List<Competition> lstScheduledComps = new List<Competition>();
        

        public ViewScheduledCompetition()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAmend_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnAddCompetition_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void RefreshCompetitionList()
        {
            lstScheduledComps.Clear();
            foreach (var CompetitionRecord in db.Competitions)
            {
                lstScheduledComps.Add(CompetitionRecord);
            }
            lstViewCompetitionSchedule.ItemsSource = lstScheduledComps;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 


            lstScheduledComps.Clear();
            foreach (var record in db.Competitions.Where(t => t.competition_status == "S" ))
            {
                lstScheduledComps.Add(record);
            }
            RefreshCompetitionList();

        }
    }
}
