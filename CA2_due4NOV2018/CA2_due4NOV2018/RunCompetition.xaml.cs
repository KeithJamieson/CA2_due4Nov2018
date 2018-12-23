using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;
using System.Data.Entity;

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

        System.DateTime currentDate = System.DateTime.Today;
        int currentyear = System.DateTime.Now.Year;

        List<Competition> lstScheduledCompetitions = new List<Competition>();
        List<Entry> CompetitionEntries = new List<Entry>();

        public int competition_id;
        string Ridergrade;
        string activeTab;
        int rider_entry_id;
        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dboperation = new DBOperation();
        private void TabP_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "P";
            RefreshList(Ridergrade);
        }


        private void TabAP_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AP";
            RefreshList(Ridergrade);
        }

        private void TabI_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "I";
            RefreshList(Ridergrade);
        }

        private void TabAI_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AI";
            RefreshList(Ridergrade);
        }

        private void TabO_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "O";
            RefreshList(Ridergrade);
        }

        private void TabAO_Selected(object sender, RoutedEventArgs e)
        {
            
            Ridergrade = "AO";
            RefreshList(Ridergrade);
        }





        private void BtnCloseCompetition_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = new Entry();
            bool place_assigned = true;
            foreach (var record in db.Entries.Where ( t=> t.competition_id == competition_id & t.points == 0   ))
            {
                MessageBox.Show($"Grade {record.grade}: Rider {record.Firstname} {record.Lastname} riding {record.Horse} for {record.clubname} has not been assigned a finishing position" );
                place_assigned = false;
            }

            if (place_assigned == true)
            {
                string competition_status = "C";  // Competition is Open if user gets to this screen
                Competition competition = new Competition();
                //check to see if competition has been closed
                foreach (var record in db.Competitions.Where(t => t.competition_id == competition_id))
                {

                    record.competition_status =competition_status;
                }
                int SaveSuccess = db.SaveChanges();

                if (SaveSuccess == 1)
                {

                    MessageBox.Show("Points Updated Successfully");
                    //lstRiders.Items.Refresh();
                }
                else
                {
                    MessageBox.Show($"Problem Closing Competition");
                }
            }
        }
        public class LeaderboardContext : DbContext
        {
            // public DbSet<Leaderboard> Orders { get; set; }

            public DbSet<Leaderboard> Leaderboard { get; set; }
            //public DbSet<Product> Products { get; set; }
            //public DbSet<OrderLine> OrderLines { get; set; }

        }
 
            
            
            

        private void BtnUpdateLeaderBoard_Click(object sender, RoutedEventArgs e)
        {
            string competition_status;
            Competition competition = new Competition();
            Leaderboard leaderboard = new Leaderboard();

            //check to see if competition has been closed
            foreach (var record in db.Competitions.Where(t => t.competition_id == competition_id))
            {

                competition_status = record.competition_status;
            }


            //var query = (from c in db.Competitions
            //             join m in db.Members on c.airc_id equals m.airc_id
            //             join en in db.Entries on m.airc_id equals en.airc_id                     
            //             where c.competition_status != "Z" && c.competition_id == competition_id &
            //             en.competition_id == c.competition_id
            //             select new
            //             {
            //                 c.competition_type,
            //                 en.grade,
            //                 en.points,
            //                 m.airc_id,
            //                 c.club_id
            //             });

            var query = from en in db.Entries
                        join c in db.Competitions on en.competition_id equals c.competition_id
                        where c.competition_id == competition_id
                        select new
                        {
                            c.competition_type,
                            en.airc_id,
                            en.points,
                            en.grade
                        };

            foreach (var record in query)
            {
                //attempt update for each record and if upate fails perform the insert
                // This way we can add new records and update existing records as needed
                Leaderboard myleaderboard = new Leaderboard();

                myleaderboard.airc_id = record.airc_id;
                myleaderboard.grade = record.grade;
                myleaderboard.competition_type = record.competition_type;
                myleaderboard.points = record.points;


                //db.SaveChanges();
                int SaveSuccess = db.SaveChanges();
              if (SaveSuccess == 0)
                {
                    db.Entry(myleaderboard).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }  
            }



            //if (competition_status == "C")
            //{
               
            //    foreach (var record in  db.Entries.Where (t=>t.competition_id == competition_id))
            //    {
            //       // leaderboard.competition_id = competition_id;
            //        leaderboard.airc_id = record.airc_id;
            //       // leaderboard.competition_type = record.competition_type;
            //        leaderboard.grade = record.grade;
            //        //nb change points to place
                 
            //        // insert record or update values of existing record. merge statement

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Competition must first be closed");

            //}
        }

        //public void InsertOrUpdate(Leaderboard leaderboard)
        //{
        //    using (var context = n())
        //    {
        //        context.Entry(blog).State = blog.BlogId == 0 ?
        //                                   EntityState.Added :
        //                                   EntityState.Modified;

        //        context.SaveChanges();
        //    }
        //}
        //public void Upsert(Leaderboard leaderboard)
        //{
        //    using (var context = new Leaderboard())
        //    {
        //        context.Leaderboard1(L).
        //        context.Entry(Leaderboards).State = leaderboard.leaderboard_id == 0 ?
        //                                   EntityState.Added :
        //                                   EntityState.Modified;

        //        context.SaveChanges();
        //    }
        //}
        private void BtnAssignFinishingPositions_Click(object sender, RoutedEventArgs e)
        {
            stkAssignFinishingPosition.Visibility = Visibility.Visible;
        }

        private void BtnAddRiderEntry_Click(object sender, RoutedEventArgs e)
        {
            AddRider addRider = new AddRider();
            addRider.competition_id = competition_id;           
            addRider.ShowDialog();
            RefreshList(Ridergrade);

        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            string competition_status = "O";  // Competition is Open if user gets to this screen
            Competition competition = new Competition();
            //check to see if competition has been closed
            foreach (var record in db.Competitions.Where (t => t.competition_id == competition_id))
            {

                competition_status = record.competition_status;
            }
            if (competition_status == "C")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Competition must first be closed and Leaderboard Updated");

            }



        }


        private void RefreshList(string Ridergrade)
        {

            try
            {
                CompetitionEntries.Clear();

                foreach (var record in db.Entries.Where(t => t.competition_id == competition_id && t.grade == Ridergrade))
                {
                    CompetitionEntries.Add(record);
                }
                lstRiders.ItemsSource = CompetitionEntries;
                lstRiders.Items.Refresh();
            }
            catch (IndexOutOfRangeException)
            {

               // throw;
            }


                     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            stkAssignFinishingPosition.Visibility = Visibility.Collapsed;
            Ridergrade = "P";
            RefreshList(Ridergrade);

        }



        private void OnTabSelected(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TabItem tab)
            {                
                e.Handled = true;
            }
        }

  

        private void LstRiders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        

            try
            {
                //NB Ridergrade holds current grade
                Entry SelectedRider = CompetitionEntries.ElementAt(lstRiders.SelectedIndex);
                if (SelectedRider.airc_id > 0)   // ensure we have a record
                {
                    tbxAIRC_ID.Text = SelectedRider.airc_id.ToString();
                    tbxLastName.Text = SelectedRider.Lastname.Trim();
                    tbxFirstName.Text = SelectedRider.Firstname.Trim();
                    tbxHorse.Text = SelectedRider.Horse.Trim();
                    tbxClub.Text = SelectedRider.clubname.Trim();
                    tbxPlace.Text = SelectedRider.points.ToString();
                    tbxEntryID.Text = SelectedRider.entry_id.ToString();
                    rider_entry_id =  SelectedRider.entry_id;

                }
            }

            catch (IndexOutOfRangeException)
            {

                // throw;
            }
            catch ( ArgumentOutOfRangeException)
            {

                // throw;
            }
            catch
            {
                throw;
            }
            

        
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            Entry entry= new Entry();
            int points = Convert.ToInt32(tbxPlace.Text.Trim());
            rider_entry_id = Convert.ToInt32(tbxEntryID.Text);
            dboperation = DBOperation.Modify;
            if (dboperation == DBOperation.Modify )
            {
                foreach (var rider in db.Entries.Where(t => t.entry_id == rider_entry_id))
                {
                    rider.airc_id = Convert.ToInt32(tbxAIRC_ID.Text.Trim());
                    rider.clubname = tbxClub.Text.Trim();
                    rider.Firstname = tbxFirstName.Text.Trim();
                    rider.Lastname = tbxLastName.Text.Trim();
                    rider.Horse = tbxHorse.Text.Trim();
                    rider.competition_id = competition_id;                    
                    rider.place = Convert.ToInt32(tbxPlace.Text);                
                    rider.grade = Ridergrade;
                    if (rider.place == 1)
                    {
                        rider.points = 14;
                    }
                    if (rider.place == 2)
                    {
                        rider.points = 12;
                    }

                    if (rider.place == 3)
                    {
                        rider.points = 10;
                    }
                    if (rider.place == 4)
                    {
                        rider.points = 8;
                    }
                    if (rider.place == 5)
                    {
                        rider.points = 6;
                    }
                    if (rider.place == 6)
                    {
                        rider.points = 4;
                    }
                    if (rider.place > 6)
                    {
                        rider.points = 2;
                    }

                }

                int SaveSuccess = db.SaveChanges();

                if (SaveSuccess == 1)
                {
                    
                    //MessageBox.Show("Points Updated Successfully");
                    lstRiders.Items.Refresh();
                }
                else
                {
                    MessageBox.Show($"Problem Saving Points");
                }            
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkAssignFinishingPosition.Visibility = Visibility.Collapsed;
            
        }
    }
}
