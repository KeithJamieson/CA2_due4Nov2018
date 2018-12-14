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
    /// Interaction logic for MyCompetition.xaml
    /// </summary>
    public partial class MyCompetition : Window
    {
        public MyCompetition()
        {
            InitializeComponent();
        }

        RELICEntities db = new RELICEntities();
        List<Entry> lstEntries = new List<Entry>();

        private void RefreshList()
        {

            lstEntries.Clear();
            foreach (var record in db.Entries.Where(t => t.competition_id == 1 ))
            {
                lstEntries.Add(record);
            }

            lstCompRiders.ItemsSource = lstEntries; 
            lstCompRiders.Items.Refresh();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
    }
}
