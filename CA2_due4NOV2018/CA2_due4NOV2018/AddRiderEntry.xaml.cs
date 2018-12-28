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
        // set up DB access
        RELICEntities db = new RELICEntities();
        // initialise variables
        public int competition_id;  
        public string competition_type;
        string SelectedClub;
        string SelectedGrade;
        int airc_id;
        // List of Riding Clubs
        List<Club> RidingClub = new List<Club>();
       
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             
            // Populate Riding Club Box

            foreach (var club in db.Clubs.Where(t => t.club_id > 1))
            {
                RidingClub.Add(club);

            }
            CboRidingClub.ItemsSource = RidingClub;
            CboRidingClub.SelectedIndex = -1;
        }


        // Minimal validation
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
             // Save Rider Entry
            if (Validate() == true)
            {
                Entry entry = new Entry();
                SaveEntry(entry);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Check Horse's name  has been Entered");
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // abort entry of this rider.
            this.Close();
        }

        private void SaveEntry(Entry entry)
        {
            
            // Save entry
           // NB: This allows us to enter riders from other regions who will not be registered on our app
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
            // Used to pick riding club from a list
            var comboBoxItem = (ComboBox)sender;
            SelectedClub = CboRidingClub.SelectedValue.ToString();
            tbxRidingClub.Text = SelectedClub;
        }

        private void CboRiderGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // used to pcik rider grade
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)CboRiderGrade.SelectedItem;
            SelectedGrade = item.Content.ToString();
        }

        private void GetMemberDetails_Click(object sender, RoutedEventArgs e)
        {

            // decided it would be useful to be able to enter riding club it and pick up rider details. We have everything except the horse.  
            int club_id;
            try
            {
                 airc_id = Convert.ToInt32(tbx_AIRC_ID.Text);
            }
            catch (Exception)
            {
                //note: This message only appears if we use the getMemberMethod with a non-registered rider
                MessageBox.Show("A valid Airc id of a registered user must be entered");
            }
          
            
            if (tbx_AIRC_ID.Text.Length > 0 )
            {
                foreach (var record in db.Members.Where (t=>t.airc_id == airc_id
                ))
                {
                    tbxRiderFirstName.Text = record.first_name;
                    tbxRiderLastName.Text = record.last_name;
                    // Select grade based on what tyoe of competition is being run
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

                    //  Assign Selected Grade to ComboxBox.
                    CboRiderGrade.SelectedValuePath = SelectedGrade;
                    tbxGrade.Text = SelectedGrade;                 

                    // pick up ridng club name
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
