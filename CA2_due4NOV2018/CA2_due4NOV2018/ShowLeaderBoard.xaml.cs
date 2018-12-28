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

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for ShowLeaderBoard.xaml
    /// </summary>
    public partial class ShowLeaderBoard : Window
    {
        public ShowLeaderBoard()
        {
            InitializeComponent();
        }
        RELICEntities db = new RELICEntities();
    
        List<leaderboard_v> lstleaderboard = new List<leaderboard_v>();     
        int currentyear = DateTime.Now.Year;
        string competition_type;
        string Ridergrade;
        private void RefreshLeaderboard(string Ridergrade )
        {

            lstleaderboard.Clear();
            //cboCompetitionType
            foreach (var  record in db.leaderboard_v.Where(t => t.competition_type == competition_type &&
                                                          t.competition_date.Year ==  currentyear && 
                                                          t.grade == Ridergrade))
            {
                lstleaderboard.Add(record);

            }

            lvwLeaderBoard.ItemsSource = lstleaderboard;
            lvwLeaderBoard.Items.Refresh();
        }









        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TabItem tab)
            {
                e.Handled = true;
            }
        }
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Functionality has not been Implemented");
        }
        private void BtnFindMyResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ridergrade = "P";
            RefreshLeaderboard(Ridergrade);
        }



        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "P";
            RefreshLeaderboard(Ridergrade);
        }

        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "AP";
            RefreshLeaderboard(Ridergrade);
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "I";
            RefreshLeaderboard(Ridergrade);
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "AI";
            RefreshLeaderboard(Ridergrade);
        }


        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "O";
            RefreshLeaderboard(Ridergrade);
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            Ridergrade = "AO";
            RefreshLeaderboard(Ridergrade);
        }

        private void CboCompetitionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboCompetitionType.SelectedItem;
            competition_type= item.Content.ToString();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //   
        //}
    }
}
