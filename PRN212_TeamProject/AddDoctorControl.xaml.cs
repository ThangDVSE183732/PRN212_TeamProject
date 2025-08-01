using BLL.Mapper;
using BLL.Services;
using DAL.Entities;
using DLL.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for AddDoctorControl.xaml
    /// </summary>
    public partial class AddDoctorControl : UserControl
    {
        private readonly DoctorService _doctorService = new DoctorService();
        private UserService userService;

        public AddDoctorControl()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtCustomerFullName.Text.Trim();
            string specialization = txtSpecialization.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtCustomerPhone.Text.Trim();
            string password = txtPassword.Password;
            string status = ((ComboBoxItem)cbCustomerStatus.SelectedItem)?.Content?.ToString() ?? "";

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!", "Error", MessageBoxButton.OK);
                return;
            }

            if (cbCustomerStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status!", "Error", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(specialization) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please Enter All Fields!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_doctorService.ExistsByEmail(email))
            {
                MessageBox.Show("Used Email!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_doctorService.ExistsByPhone(phone))
            {
                MessageBox.Show("Used Phone Number!", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                var newDoctor = new Doctor
                {
                    DoctorName = fullName,
                    Specialization = specialization,
                    Email = email,
                    Phone = phone,
                    Status = status,
                    Password = password,
                };
                User newUser = UserMapper.ToDoctor(email, password);
                _doctorService.CreateDoctor(newDoctor);
                userService.CreateUser(newUser);
                MessageBox.Show("Success!");

                ClearFields();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Exception Catched!: " + ex.Message, "Exception", MessageBoxButton.OK);
            }
        }

        private void ClearFields()
        {
            txtCustomerFullName.Clear();
            txtSpecialization.Clear();
            txtEmail.Clear();
            txtCustomerPhone.Clear();
            txtPassword.Clear();
            cbCustomerStatus.SelectedIndex = 0;
        }
    }
}
