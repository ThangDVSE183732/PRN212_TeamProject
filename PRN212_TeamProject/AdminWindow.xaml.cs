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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public User Auth { get; set; }

        public AdminWindow()
        {
            InitializeComponent();
            MainContentControl.Content = new UserManagement();
        }

        private void UserManagment_Click(object sender, MouseButtonEventArgs e)
        {
            MainContentControl.Content = new UserManagement();
        }

        private void DoctorManagment_Click(object sender, MouseButtonEventArgs e)
        {
            MainContentControl.Content = new DoctorManagement();
        }



        //private void navToServicePage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    ServiceTestingWindow serviceTestingWindow = new ServiceTestingWindow();
        //    serviceTestingWindow.Show();
        //    this.Close();
        //}

        //private void navToLoginPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBoxResult msg = MessageBox.Show("Bạn có muốn thật sự đăng xuất không?", "LOG OUT", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //    if (msg == MessageBoxResult.Yes)
        //    {
        //        LoginWindow loginWindow = new LoginWindow();
        //        loginWindow.Show();
        //        this.Close();
        //    }
        //}

        //private void navToUserManagementPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    UserManagement userManagement = new UserManagement();
        //    userManagement.Auth = Auth;
        //    userManagement.Show();
        //    this.Close();
        //}
    }
}
