using BLL.Services;
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
using System.Windows.Shapes;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User _user;
        private PatientService patientService;
        
        public UserWindow(User user)
        {
            InitializeComponent();
            _user = user;
            patientService = new PatientService();
        }

        private void PatientProfile_Click(object sender, MouseButtonEventArgs e)
        {
            Patient? patient = patientService.GetPatientProfile(_user.Email, _user.Password);
            UserContentControl.Content = new PatientProfileManagement(patient);
        }

        private void UserProfile_Click(object sender, MouseButtonEventArgs e)
        {
            UserContentControl.Content = new UserProfileControl(_user);

        }

        private void BookingService_Click(object sender, MouseButtonEventArgs e)
        {
            Patient? patient = patientService.GetPatientProfile(_user.Email, _user.Password);
            UserContentControl.Content = new BookingServiceControl(patient);

        }

        private void LogOut_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out",
                                              MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
