using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;

namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for AddRider.xaml
    /// </summary>
    public partial class AddRider : Window
    {
        public AddRider()
        {
            InitializeComponent();
        }

        RELICEntities db = new RELICEntities();
        public int competition_id;
        public string competition_type;
        string SelectedClub;
        string SelectedGrade;
        int airc_id;
        List<Club> RidingClub = new List<Club>();
       
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             


            foreach (var club in db.Clubs.Where(t => t.club_id > 1))
            {
                RidingClub.Add(club);

            }
            CboRidingClub.ItemsSource = RidingClub;
            CboRidingClub.SelectedIndex = -1;
        }


        private bool Validate()
        {
            bool validated = true;
            if (tbx_AIRC_ID.Text.Length == 0)
            {
                validated=false;
            }
            if (tbxNameOfHorse.Text.Length == 0)
            {
                validated = false;
            }
            if (tbxGrade.Text.Length == 0)
            {
                validated = false;
            }
            return validated;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == true)
            {
                Entry entry = new Entry();
                SaveEntry(entry);
                this.Close();
            }
            else
            {
                MessageBox.Show("Some items need to be Entered");
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveEntry(Entry entry)
        {
            entry.airc_id = System.Convert.ToInt32(tbx_AIRC_ID.Text);
            entry.Firstname = tbxRiderFirstName.Text;
            entry.Lastname = tbxRiderLastName.Text;
            entry.Horse = tbxNameOfHorse.Text;
            entry.grade = SelectedGrade;
            entry.clubname=tbxRidingClub.Text;
            entry.competition_id = competition_id;
            entry.points = 0;
            db.Entry(entry).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
          
        }

        private void CboRidingClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            SelectedClub = CboRidingClub.SelectedValue.ToString();
            tbxRidingClub.Text = SelectedClub;
        }

        private void CboRiderGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)CboRiderGrade.SelectedItem;
            SelectedGrade = item.Content.ToString();
        }

        private void GetMemberDetails_Click(object sender, RoutedEventArgs e)
        {

            
            int club_id;
            try
            {
                 airc_id = Convert.ToInt32(tbx_AIRC_ID.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("A valid Airc id of a registered user must be entered");
            }
          
            
            if (tbx_AIRC_ID.Text.Length > 0 )
            {
                foreach (var record in db.Members.Where (t=>t.airc_id == airc_id
                ))
                {
                    tbxRiderFirstName.Text = record.first_name;
                    tbxRiderLastName.Text = record.last_name;
                    if (tbxCompetitionType.Text.Trim() == "DR")
                    {
                        SelectedGrade = record.DR;
                    }
                    if (tbxCompetitionType.Text.Trim() == "SJ")
                    {
                        SelectedGrade = record.SJ;
                    }
                    if (tbxCompetitionType.Text.Trim() == "XC")
                    {
                        SelectedGrade = record.XC;
                    }

                    CboRiderGrade.SelectedValuePath = SelectedGrade;
                    tbxGrade.Text = SelectedGrade;                 


                    club_id = record.club_id;
                    foreach (var club in db.Clubs.Where(t=>t.club_id==club_id))
                    {
                        tbxRidingClub.Text = club.clubname;
                    }
    }
            }
            else
            {
                MessageBox.Show("A valid Airc id of a registered user must be entered");
            }
        }


    }
}
