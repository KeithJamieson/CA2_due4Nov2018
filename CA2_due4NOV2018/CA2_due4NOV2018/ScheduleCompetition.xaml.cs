﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for SchdeuleCompetition.xaml
    /// </summary>
    public partial class ScheduleCompetition : Window
    {

        RELICEntities db = new RELICEntities();

        List<Member> memberslist = new List<Member>();
        int club_id=1004;
        int airc_id;
  //      int competitionSecretary;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            memberslist.Clear();
            foreach (var member in db.Members.Where(t => t.club_id > 0  ))
            {                      
                memberslist.Add(member);                
            }
            cboCompetitionSecretary.ItemsSource = memberslist;
        }

        public ScheduleCompetition()
        {
            InitializeComponent();
        }
        string CompetitionDiscipline = "";

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Competition competition = new Competition();

            competition.venue = tbxCompetitionVenue.Text.Trim();
            competition.competition_date =Convert.ToDateTime(dpkCompetitionDate.ToString());
            competition.competition_type = CompetitionDiscipline;
            competition.competition_status = "S";
            competition.club_id = club_id;
            competition.airc_id = airc_id;
            ScheduleCompetitionSave(competition);
            MessageBox.Show("Competition has been successfully scheduled");
            this.Close();
        }

        private static Competition GetCompetition(Competition competition)
        {
            return competition;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cboCompetitionType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBoxItem = sender;
            ComboBoxItem item = (ComboBoxItem)cboCompetitionType.SelectedItem;
            CompetitionDiscipline = item.Content.ToString();
        }

        private void cboCompetitionSecretary_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            string airc_id_str = cboCompetitionSecretary.SelectedValue.ToString();             
            airc_id = Convert.ToInt32(airc_id_str);
        }

        private void ScheduleCompetitionSave(Competition competition)
        {
        
            db.Entry(competition).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

    }
}
