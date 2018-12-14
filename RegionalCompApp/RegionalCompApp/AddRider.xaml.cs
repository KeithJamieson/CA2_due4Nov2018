using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using RegionalCompApp;

namespace RegionalCompApp
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



            foreach (var club in db.Clubs.Where(t => t.club_id > 1))
            {
                RidingClub.Add(club);

            }
            cb0RidingClub.ItemsSource = RidingClub;
        }



        List<Club> RidingClub = new List<Club>();
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = new Entry();
            saveEntry(entry);
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveEntry(Entry entry)
        {
            entry.airc_id = System.Convert.ToInt32(tbx_AIRC_ID.Text);
            entry.Firstname = tbxRiderFirstName.Text;
            entry.Lastname = tbxRiderLastName.Text;
            entry.Horse = tbxNameOfHorse.Text;
            entry.grade = SelectedGrade;
            entry.clubname = SelectedClub;
            entry.competition_id = competition_id;
            entry.points = 0;
            db.Entry(entry).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

        }

        private void cb0RidingClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            SelectedClub = cb0RidingClub.SelectedValue.ToString();
        }

        private void cb0RiderGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cb0RiderGrade.SelectedItem;
            SelectedGrade = item.Content.ToString();
        }
    }
}
