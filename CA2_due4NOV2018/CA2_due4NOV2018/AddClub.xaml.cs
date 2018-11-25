using System.Windows;


namespace CA2_due4NOV2018
{
    /// <summary>
    /// Interaction logic for AddClub.xaml
    /// </summary>
    public partial class AddClub : Window
    {

        RELICEntities db = new RELICEntities();
        public AddClub()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string clubname = tbxClubName.Text;
            Club(clubname);
            //this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Club(string clubname)
        {
            Club newclub = new Club();           
            //club.clubname = clubname;
        }

        //private void SaveClub(Club)
        //{
        //    db.Entry(Club).State = System.Data.Entity.EntityState.Added;
        //    //            db.Entry(Member).State = System.Data.Entity.EntityState.Added;
        //    db.SaveChanges();
        //}
    }
}
