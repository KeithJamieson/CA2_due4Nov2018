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
using System.ComponentModel;
using System.Windows.Data;
namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for MyCompetition.xaml
    /// </summary>
    public partial class MyCompetition : Window
    {
        public MyCompetition()
        {
            InitializeComponent();
        }
        System.DateTime currentDate = System.DateTime.Today;
        int currentyear = System.DateTime.Now.Year;
        RELICEntities db = new RELICEntities();
        List<Entry> lstEntries = new List<Entry>();
        List<RiderEntry> riderEntries = new List<RiderEntry>();
        List<Competition> lstScheduledCompetitions = new List<Competition>();
        private void RefreshList()
        {

            lstEntries.Clear();
            foreach (var record in db.RiderEntries.Where(t => t.competition_id == 1 ))
            {
                riderEntries.Add(record);
            }

            lstMyRiders.ItemsSource = riderEntries;
            lstMyRiders.Items.Refresh();
        }


        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
            RefreshCompetitionList();
        }

        private void RefreshCompetitionList()
        {
            lstScheduledCompetitions.Clear();

            foreach (var record in db.Competitions.Where(t => t.competition_status == "S" && t.competition_date >= currentDate && t.competition_date.Year == currentyear))
            {

                lstScheduledCompetitions.Add(record);

            }
            lstViewCompetitionSchedule.ItemsSource = lstScheduledCompetitions;
            lstViewCompetitionSchedule.Items.Refresh();

        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
