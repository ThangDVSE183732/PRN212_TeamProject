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
    /// Interaction logic for DoctorManagement.xaml
    /// </summary>
    public partial class DoctorManagement : UserControl
    {
        public DoctorManagement()
        {
            InitializeComponent();
            DoctorContentControl.Content = new SearchDoctorControl();

        }

        private void SearchDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorContentControl.Content = new SearchDoctorControl();

        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorContentControl.Content = new AddDoctorControl();

        }

        private void UpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorContentControl.Content = new UpdateDoctorControl();

        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
