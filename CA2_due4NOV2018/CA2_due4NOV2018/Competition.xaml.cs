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
    /// Interaction logic for OpenCompetition.xaml
    /// </summary>
    public partial class Competition : Window
    {

        RELICEntities db = new RELICEntities();
        //List<Competition> lstCompetitors = new List<Competition>();
        List<Entry> lstEntries = new List<Entry>();
        string activeTab;


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

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
           
        }

       

        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabP";
        }


        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAP";
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabI";
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAI";
        }

        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabO";
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAO";
        }

        //private void RefreshEntryList()
        //{
        //    lstEntryList.ItemsSource = Competition;
        //    entries.Clear();
        //    foreach (var entry in db.Entry)
        //    {
        //        Entry.Add(Rider)
        //    }
        //}   
    }
}
