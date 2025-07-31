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
    /// Interaction logic for UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : UserControl
    {
        private readonly UserService _userService = new UserService();
        private User _selectedUser;

        public UpdateClient(User user)
        {
            InitializeComponent();
            _selectedUser = user;
            LoadUser();
        }

        private void LoadUser()
        {
            txtId.Text = _selectedUser.UserId.ToString();
            txtCustomerFullName.Text = _selectedUser.FullName;
            txtEmailAdress.Text = _selectedUser.Email;
            txtCustomerPhone.Text = _selectedUser.Phone;
            txtCustomerBD.SelectedDate = _selectedUser.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
            txtAddress.Text = _selectedUser.Address;
            txtPassword.Password = _selectedUser.Password;
            cbCustomerStatus.SelectedIndex = _selectedUser.Status == "Active" ? 0 : 1;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtCustomerFullName.Text.Trim();
            string email = txtEmailAdress.Text.Trim();
            string phone = txtCustomerPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Password;
            DateTime? dob = txtCustomerBD.SelectedDate;
            string status = ((ComboBoxItem)cbCustomerStatus.SelectedItem)?.Content?.ToString() ?? "";

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password) || dob == null)
            {
                MessageBox.Show("Please Enter All Fields!", "Error", MessageBoxButton.OK);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_selectedUser.Email != email && _userService.ExistsByEmail(email))
            {
                MessageBox.Show("Used Email!", "Error", MessageBoxButton.OK);
                return;
            }

            if (_selectedUser.Phone != phone && _userService.ExistsByPhone(phone))
            {
                MessageBox.Show("Used Phone Number!", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                var updatedUser = new User
                {
                    UserId = _selectedUser.UserId,
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    DateOfBirth = DateOnly.FromDateTime(dob.Value),
                    Password = password,
                    Status = status,
                    RoleId = _selectedUser.RoleId
                };

                bool isUpdated = _userService.UpdateUser(updatedUser);
                if (isUpdated)
                {
                    MessageBox.Show("Update User Successfully!", "Success", MessageBoxButton.OK);
                } else
                {
                    MessageBox.Show("Update Fail!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

    }
}
