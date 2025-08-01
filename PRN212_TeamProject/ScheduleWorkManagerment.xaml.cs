using DAL.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for ScheduleWorkManagerment.xaml
    /// </summary>
    public partial class ScheduleWorkManagerment : UserControl
    {
        private Doctor _doctor;
        public ScheduleWorkManagerment(Doctor doctor)
        {
            InitializeComponent();
            _doctor = doctor;
            WorkContentControl.Content = new ViewScheduleWorkControl(_doctor);
        }

        private void btnViewWork_Click(object sender, RoutedEventArgs e)
        {
            WorkContentControl.Content = new ViewScheduleWorkControl(_doctor);
        }
    }
}
