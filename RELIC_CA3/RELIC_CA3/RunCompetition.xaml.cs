using RELICDBLibrary;
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

namespace RELIC_CA3
{
    /// <summary>
    /// Interaction logic for RunCompetition.xaml
    /// </summary>
    public partial class RunCompetition : Window
    {
        public RunCompetition()
        {
            InitializeComponent();
        }
        RELICEntities db = new RELICEntities();
        List<Entry> lstCompetitionEntries = new List<Entry>();
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

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddRiderEntry_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCloseCompetition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdateLeaderBoard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshList(string Ridergrade)
        {

            lstCompetitionEntries.Clear();

            //foreach (var record in db.Entries.Where(t => t.competition_id == competition_id && t.grade == Ridergrade))
            foreach (var record in db.Entries.Where(t => t.competition_id >0))

                {
                    lstCompetitionEntries.Add(record);
            }
            lvwRiders.ItemsSource = lstCompetitionEntries;
            lvwRiders.Items.Refresh();
        }
            private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }
    }
}
