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
    /// Interaction logic for Window1.xaml
    /// </summary>
    ///  

    public partial class Window1 : Window
    {
        RELICEntities db = new RELICEntities();
        List<Competition> lstScheduledCompetitions = new List<Competition>();
        public Window1()
        {
            InitializeComponent();

        }

        private void RefreshCompetitionList()
        {
           lstScheduledCompetitions.Clear();

            foreach (var record in db.Competitions.Where(t => t.competition_status == "S" ))
            {

                lstScheduledCompetitions.Add(record);

            }
            MyCompetitionSchedule.ItemsSource = lstScheduledCompetitions;
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstViewCompetitionSchedule.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("competition_date", ListSortDirection.Ascending));
            MyCompetitionSchedule.Items.Refresh();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCompetitionList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
