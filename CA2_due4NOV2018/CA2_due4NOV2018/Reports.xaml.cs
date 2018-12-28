using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        string report;
       // string v_competition_name = "Christmas Showjumping League";
        int selectedYear = DateTime.Now.Year;
        DateTime selectedDate = DateTime.Today;
        public Reports()
        {
            InitializeComponent();
        }
        RELICEntities db = new RELICEntities();       
        List<EntrantsPerCompetition_v> lstEntrantsPerCompetitions = new List<EntrantsPerCompetition_v>();
        private void RunReport_Click(object sender, RoutedEventArgs e)
        {
            // get year in int a scan not use conversiuon functions in lambda expression.
            selectedYear = Convert.ToInt32( tbxYear.Text.Trim());
            stkReportEntrantsperCompetition.Visibility = Visibility.Visible;         
            if (report == "Entrants per Competition")
            {
                // clear list box
            
                lstEntrantsPerCompetitions.Clear();
                foreach (var record in db.EntrantsPerCompetition_v.Where(t=>t.competition_date.Year == selectedYear))
                {
                    lstEntrantsPerCompetitions.Add(record);
                }
                lvwEntrantsPerComp.ItemsSource = lstEntrantsPerCompetitions;
                lvwEntrantsPerComp.Items.Refresh();
            }
            else
            if (report == "Entrants per Competition per Club")
            {
                MessageBox.Show("Report is not available");
            }
            else
            if (report == "Total Entrants")
            {
                MessageBox.Show("Report is not available");

            }
            else
            {
                MessageBox.Show("Selected report is notavailable");
            }
        }
   

        private void BtnResetScreen_Click(object sender, RoutedEventArgs e)
        {
            stkReportEntrantsperCompetition.Visibility = Visibility.Collapsed;
            selectedYear = DateTime.Now.Year;

            tbxYear.Text = Convert.ToString(selectedYear);
                  
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void CboReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)CboReport.SelectedItem;
            report= item.Content.ToString();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            selectedYear = DateTime.Now.Year;
            tbxYear.Text = Convert.ToString(selectedYear);

        }

     
    }
}
