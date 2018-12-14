using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;


namespace RegionalCompApp
{
    /// <summary>
    /// Interaction logic for RunCompetition.xaml
    /// </summary>
    public partial class RunCompetition : Window
    {


        RELICEntities db = new RELICEntities();
        List<Entry> lstEntries = new List<Entry>();       

        public int competition_id;
        string Ridergrade;
        public RunCompetition()
        {
            InitializeComponent();
        }
 
        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }


        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            
            RefreshList(Ridergrade);
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            
            RefreshList(Ridergrade);
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            
            RefreshList(Ridergrade);
        }

        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            
            RefreshList(Ridergrade);
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            
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

        }

        private void BtnAddRiderEntry_Click(object sender, RoutedEventArgs e)
        {
            AddRider addRider = new AddRider();
            addRider.competition_id = competition_id;
            addRider.ShowDialog();
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshList(string Ridergrade)
        {

            lstEntries.Clear();

            foreach (var record in db.Entries.Where(t => t.competition_id == competition_id && t.grade == Ridergrade))
            {
                lstEntries.Add(record);
            }
            lvwRiders.ItemsSource = lstEntries;
            lvwRiders.Items.Refresh();

      
            //items.Add(new User() { Name = "John Doe", Age = 42 });
            //items.Add(new User() { Name = "Jane Doe", Age = 39 });
            //items.Add(new User() { Name = "Sammy Doe", Age = 13 });
            //lvDataBinding.ItemsSource = items;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
    }
}
