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
    /// Interaction logic for ModifyMyDetails.xaml
    /// </summary>
    public partial class ModifyUserDetails : Window
    {
        RELICEntities db = new RELICEntities();

        public string Username;
        int airc_id;
        public ModifyUserDetails()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            foreach (var member in db.Members.Where(t=>t.airc_id == airc_id))
            {
              
                member.email = tbxEmail.Text.Trim();
                member.phone = tbxPhone.Text.Trim();
                member.first_name = tbxFirstname.Text.Trim();
                member.last_name = tbxLastName.Text.Trim();
                member.DR = cboDR.Text.Trim();
                member.SJ = cboSJ.Text.Trim();
                member.XC = cboXC.Text.Trim();
                member.member_status = "A";  // set to approved but will be done by secretary in future not member)

            }
            int SaveSuccess = db.SaveChanges();
            if (SaveSuccess == 1)
            {
                MessageBox.Show("Details have been successfully updated");
                this.Close();
            }
            else
            {
                MessageBox.Show("problem upating member details");
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CboDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CboSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CboXC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private enum SelectedGrade
        {
            P, AP, I,AI,O,AO
        };

        int getSelectedIndex(string grade)
        {
          if (grade == "P")
          {
                    return 0;
          }
            if (grade == "AP")
            {
                return 1;
            }
            if (grade == "I")
            {
                return 2;
            }
            if (grade == "AI")
            {
                return 3;
            }
            if (grade == "O")
            {
                return 4;
            }
            if (grade == "AO")
            {
                return 5;
            }
            return -1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            var query = from u in db.Users
                        join m in db.Members on u.airc_id equals m.airc_id
                        where u.username == Username 
             select new { m.first_name,
                          m.last_name,
                          m.member_status,
                          m.airc_id,
                          m.phone,
                          m.email,
                          m.DR,
                          m.SJ,
                         m.XC};

             

         
            foreach (var record in query)
            {

                airc_id = record.airc_id;
                tbxFirstname.Text = record.first_name;
                tbxLastName.Text = record.last_name;
                tbxPhone.Text = record.phone;
                tbxEmail.Text = record.email;
                tbxAIRC_ID.Text = Convert.ToString(record.airc_id);

                
                cboDR.SelectedIndex = getSelectedIndex(record.DR);
                cboSJ.SelectedIndex = getSelectedIndex(record.SJ);
                cboXC.SelectedIndex = getSelectedIndex(record.XC);

                if (record.member_status != "A" )
                {
                    cboDR.IsEnabled = false;
                    cboSJ.IsEnabled = false;
                    cboXC.IsEnabled = false;
                }
               
            }

        }
    }
}
