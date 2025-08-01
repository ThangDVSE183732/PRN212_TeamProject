using BLL.Mapper;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService _userService;
        private PatientService _patientService;

        private bool isPasswordVisible = false;

        public RegisterWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            _patientService = new PatientService();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string email = txtEmail.Text;
                string password = txtPassword.Password;
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) 
                {
                    MessageBox.Show("Please fill all field", "Register Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                User? existedUser = _userService.GetUserAccount(email, password);

                if (existedUser != null)
                {
                    MessageBox.Show("Account is existed in system", "Register Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }
                User newUser = UserMapper.ToUser(email, password);
                Patient patient = PatientMapper.ToPatient(email, password);
                if (newUser != null)
                {
                    _userService.CreateUser(newUser);
                    _patientService.CreatePatient(patient);
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during registration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void AlreadyHaveAccount_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to login with existed account?", "Warning",
                                              MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
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
