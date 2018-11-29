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
            RegisterClub(clubname);

            db.SaveChanges();
            this.Close();
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterClub(string clubname)
        {
            Club newclub = new Club();
            newclub.clubname = clubname;
            db.Entry(newclub).State = System.Data.Entity.EntityState.Added;
        }

        private void RegisterUser(int airc_id, string username, string password)
        {


            User user = new User();
            user.airc_id = airc_id;
            user.username = username;
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }
        //private void SaveClub(Club)
        //{
        //    db.Entry(Club).State = System.Data.Entity.EntityState.Added;
        //    //            db.Entry(Member).State = System.Data.Entity.EntityState.Added;
        //    db.SaveChanges();
        //}
    }
}
