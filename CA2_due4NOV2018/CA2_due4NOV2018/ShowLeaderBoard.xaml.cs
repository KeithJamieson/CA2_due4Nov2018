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
        //List<Leaderboard> lstleaderboard = new List<Leaderboard>();
        List<leaderboard_v> lstleaderboard = new List<leaderboard_v>();
       // leaderboard_v leaderboard_v = new leaderboard_v();        
        int currentyear = DateTime.Now.Year;
        string competition_type;
        private void RefreshLeaderboard()
        {
            //competition_type = cboCompetitionType.SelectedValue.ToString();
            competition_type = "DR";
            lstleaderboard.Clear();
            //cboCompetitionType
            foreach (var  record in db.leaderboard_v.Where(t => t.competition_type == competition_type &&
                                                          t.competition_date.Year ==  currentyear))
            {
                lstleaderboard.Add(record);

            }

            lvwLeaderBoard.ItemsSource = lstleaderboard;
            lvwLeaderBoard.Items.Refresh();
        }
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnFindMyResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshLeaderboard();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //   
        //}
    }
}
