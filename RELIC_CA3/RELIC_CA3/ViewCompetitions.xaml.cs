
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
    /// Interaction logic for ViewCompetitions.xaml
    /// </summary>
    public partial class ViewCompetitions : Window
    {
        public ViewCompetitions()
        {
            InitializeComponent();
        }
        RELICEntities db = new RELICEntities();

        List<ViewCompetitions> lstViewCompetitions = new List<ViewCompetitions>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();

        }

        private void RefreshList()
        {
            lstViewCompetitions.Clear();
            var query = (from c in db.Competitions                       
                         select new
                         {
                             c.competition_date,
                             c.competition_id,
                             c.competition_name,
                             c.competition_type,
                             c.venue,
                             Secretary = c.Member.first_name + " " + c.Member.last_name,
                             hosting_club = c.Club.clubname                             
                             c.club_id
                         });

            foreach (var record in query)
            {

                lstViewCompetitions.Add(record);

            }
            lvwCompetitions.ItemsSource = lstViewCompetitions;
            lvwCompetitions.Items.Refresh();
        }
    }
}
