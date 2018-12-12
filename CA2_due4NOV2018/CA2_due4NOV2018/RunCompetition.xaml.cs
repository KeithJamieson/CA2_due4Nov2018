﻿using System;
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
    public partial class RunCompetition : Window
    {
        public RunCompetition()
        {
            InitializeComponent();
        }
        RELICEntities db = new RELICEntities();
        List<Entry> lstEntries = new List<Entry>();       

        public int competition_id;
        string Ridergrade;
        string activeTab;
        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabP";
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }


        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAP";
            RefreshList(Ridergrade);
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabI";
            RefreshList(Ridergrade);
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAI";
            RefreshList(Ridergrade);
        }

        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabO";
            RefreshList(Ridergrade);
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            activeTab = "tabAO";
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
            lstCompetitors.ItemsSource = lstEntries;
            lstCompetitors.Items.Refresh();
            //lstRiders.Items.Refresh();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }
    }
}