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
    /// Interaction logic for ScheduledCompetitions.xaml
    /// </summary>
    public partial class ScheduledCompetitions : Window
    {
        public ScheduledCompetitions()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAmend_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
