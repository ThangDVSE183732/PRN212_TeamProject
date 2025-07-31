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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _userService;

        private bool isPasswordVisible = false;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User? user = _userService.GetUserAccount(email, password);

            if (user == null)
            {
                MessageBox.Show("Invalid Email Or Password", "Login Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                return;
            }
            if (user.RoleId != null)
            {
                switch(user.RoleId)
                {
                    case 1:
                        AdminWindow admin = new AdminWindow();
                        admin.Show();
                        this.Close();
                        break;
                    case 2:
                        ManagerWindow manager = new ManagerWindow();
                        manager.Show();
                        this.Close();
                        break;
                    case 3:
                        DoctorWindow doctor = new DoctorWindow();
                        doctor.Show();
                        this.Close();
                        break;
                    case 4:
                        StaffWindow staff = new StaffWindow();
                        staff.Show();
                        this.Close();
                        break;
                    case 5:
                        UserWindow client = new UserWindow(user);
                        client.Show();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show("You do not have permission to access this application.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }
            }
           
            this.Close();
        }

        private void btnShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!isPasswordVisible)
            {
                // Show password in TextBox
                txtPasswordVisible.Text = txtPassword.Password;
                txtPasswordVisible.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Hide password again
                txtPassword.Password = txtPasswordVisible.Text;
                txtPassword.Visibility = Visibility.Visible;
                txtPasswordVisible.Visibility = Visibility.Collapsed;
            }

            isPasswordVisible = !isPasswordVisible;
        }
    }
}
