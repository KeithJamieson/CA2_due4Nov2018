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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainDashboard : Window
    {
        
        //RELICEntities db = new RELICEntities();
        //Member member = new Member();


        public MainDashboard()
        {
            InitializeComponent();
            //tbxusername = RELICEntities.Member;



            //  foreach (var user in db.users.Where(t => t.username == currentUser && t.userpassword == currentPassword))
 

               // MessageBox.Show($"activeTab");
                
              
            
        }

      
        private void BtnChangeMyPassword_Click(object sender, RoutedEventArgs e)
        {

        }

 



        private void BtnModifyMyDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnModifyMemberDetails_Click(object sender, RoutedEventArgs e)
        {

        }



        private void BtnViewLeaderboard_Click(object sender, RoutedEventArgs e)
        {

        }
    

    private void BtnViewReports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnListNewMembers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnViewScheduledCompetitions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddClub_Click(object sender, RoutedEventArgs e)
        {

        }

             
        private void BtnOpenCompetition_Click(object sender, RoutedEventArgs e)
        {

        }
        //private void CheckUserAccess(Db.User user)
        //{
        //    if (user.)
        //    {

        //    }
        //}
    }
}
