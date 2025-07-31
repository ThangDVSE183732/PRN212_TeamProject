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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for AddClientControl.xaml
    /// </summary>
    public partial class AddClientControl : UserControl
    {
        private readonly UserService _userService = new UserService();

        public AddClientControl()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtCustomerFullName.Text.Trim();
            string email = txtEmailAdress.Text.Trim();
            string phone = txtCustomerPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Password;
            DateTime? dob = txtCustomerBD.SelectedDate;
            string status = ((ComboBoxItem)cbCustomerStatus.SelectedItem)?.Content?.ToString() ?? "";

            if (cbCustomerStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status!", "Error", MessageBoxButton.OK);
                return;
            }

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || dob == null)
            {
                MessageBox.Show("Please Enter All Fields!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_userService.ExistsByEmail(email))
            {
                MessageBox.Show("Used Email!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_userService.ExistsByPhone(phone))
            {
                MessageBox.Show("Used Phone Number!", "Error", MessageBoxButton.OK);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                var newUser = new User
                {
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    DateOfBirth = DateOnly.FromDateTime(dob.Value),
                    Password = password,
                    Status = status,
                    RoleId = 5
                };
                _userService.CreateUser(newUser);
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
            txtEmailAdress.Clear();
            txtCustomerPhone.Clear();
            txtAddress.Clear();
            txtPassword.Clear();
            txtCustomerBD.SelectedDate = null;
            cbCustomerStatus.SelectedIndex = 0;
        }
    }

}
