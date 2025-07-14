using BLL.Services;
using BLL.utils;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private UserService _userService;

        private RoleService _roleService;

        private User _selectedUser;

        public User Auth { get; set; }

        public UserManagement()
        {
            InitializeComponent();
            _userService = new UserService();
            _roleService = new RoleService();
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        public void LoadUsers()
        {
            dgUser.ItemsSource = _userService.GetUsers();
        }

        public void LoadRoles()
        {
            cbRole.ItemsSource = _roleService.GetRoles();
            cbRole.DisplayMemberPath = "RoleName";
            cbRole.SelectedValuePath = "RoleId";
            cbRole.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
            LoadRoles();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullName.Text.Trim();
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter full name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (fullName.Length > 50)
            {
                MessageBox.Show("Full name must be less than or equal to 50 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string email = Email.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter email.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string address = Address.Text.Trim();
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter address.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (address.Length > 100)
            {
                MessageBox.Show("Address must be less than or equal to 100 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string phoneNumber = PhoneNumber.Text.Trim();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please enter phone number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Định dạng đúng 10 chữ số và bắt đầu bằng đầu số hợp lệ tại VN
            if (!Regex.IsMatch(phoneNumber, @"^(03|05|07|08|09)\d{8}$"))
            {
                MessageBox.Show("Phone number must start with 03, 05, 07, 08, or 09 and contain exactly 10 digits.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateOnly dob;

            if (DateOfBirth.SelectedDate.HasValue)
            {
                dob = DateOnly.FromDateTime(DateOfBirth.SelectedDate.Value);
            }
            else
            {
                // Ngày chưa được chọn -> xử lý lỗi hoặc gán mặc định
                MessageBox.Show("Please select a date of birth.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_userService.ExistsByEmail(email))
            {
                MessageBox.Show("Email is already registered. Please use another email.", "Duplicate Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_userService.ExistsByPhone(phoneNumber))
            {
                MessageBox.Show("Phone number is already registered. Please use another phone number.", "Duplicate Phone", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User user = new User();
            user.FullName = fullName;
            user.Address = address;
            user.Email = email;
            user.Phone = phoneNumber;
            user.DateOfBirth = dob;
            user.RoleId = (int) cbRole.SelectedValue;
            user.Password = PasswordUtil.GenerateRandomPassword(10);
            user.Status = "ACTIVE";
            try
            {
                var emailService = new EmailService();
                emailService.SendMail(email,
                    "Your Account Has Been Created",
                    $@"
                       <p>Dear {fullName},</p>
                       <p>Your account has been created successfully.</p>
                       <p><b>Login email:</b> {email}</p>
                       <p><b>Password:</b> {user.Password}</p>
                       <p>Please change your password after your first login for security reasons.</p>
                     <br/>
                       <p>Best regards,<br/>The Administration Team</p>
                    ");
                _userService.CreateUser(user);
                LoadUsers();
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SEND EMAIL ERROR:\n" + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Do you want to delete this user?", "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msg == MessageBoxResult.Yes)
            {
                if (dgUser.SelectedItem is User user)
                {
                    if (user.RoleId == 1)
                    {
                        if (user.Email == "huatri1235@gmail.com")
                        {
                            MessageBox.Show("CANNOT DELETE THIS ADMIN ACCOUNT!", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        if (Auth.Email != "huatri1235@gmail.com")
                        {
                            MessageBox.Show("CANNOT DELETE ADMIN ACCOUNT (Except ADMIN huatri1235@gmail.com)", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    _userService.DeleteUser(user);

                    LoadUsers();
                    LoadRoles();
                }
            }

        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUser.SelectedItem is User user)
            {
                FullName.Text = user.FullName;
                Address.Text = user.Address;
                Email.Text = user.Email;
                PhoneNumber.Text = user.Phone;
                DateOfBirth.SelectedDate = user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
                cbRole.SelectedValue = user.RoleId;

                btnDelete.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
                _selectedUser = user;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullName.Text.Trim();

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter full name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (fullName.Length > 50)
            {
                MessageBox.Show("Full name must be less than or equal to 50 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string email = Email.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter email.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string address = Address.Text.Trim();
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter address.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (address.Length > 100)
            {
                MessageBox.Show("Address must be less than or equal to 100 characters.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string phoneNumber = PhoneNumber.Text.Trim();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please enter phone number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Định dạng đúng 10 chữ số và bắt đầu bằng đầu số hợp lệ tại VN
            if (!Regex.IsMatch(phoneNumber, @"^(03|05|07|08|09)\d{8}$"))
            {
                MessageBox.Show("Phone number must start with 03, 05, 07, 08, or 09 and contain exactly 10 digits.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateOnly dob;

            if (DateOfBirth.SelectedDate.HasValue)
            {
                dob = DateOnly.FromDateTime(DateOfBirth.SelectedDate.Value);
            }
            else
            {
                // Ngày chưa được chọn -> xử lý lỗi hoặc gán mặc định
                MessageBox.Show("Please select a date of birth.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_userService.ExistsByEmailExceptId(email, _selectedUser.UserId))
            {
                MessageBox.Show("Email is already registered. Please use another email.", "Duplicate Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_userService.ExistsByPhoneExceptId(phoneNumber, _selectedUser.UserId))
            {
                MessageBox.Show("Phone number is already registered. Please use another phone number.", "Duplicate Phone", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User user = new User();
            user.FullName = fullName;
            user.Address = address;
            user.Email = email;
            user.Phone = phoneNumber;
            user.DateOfBirth = dob;
            user.RoleId = (int) cbRole.SelectedValue;
            user.Password = _selectedUser.Password;
            user.Status = _selectedUser.Status;
            user.UserId = _selectedUser.UserId;
            _userService.UpdateUser(user);
            LoadUsers();
        }

        private void btnClearInput_Click(object sender, RoutedEventArgs e)
        {
            dgUser.UnselectAll();
            _selectedUser = null;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            FullName.Clear();
            Address.Clear();
            Email.Clear();
            PhoneNumber.Clear();
            DateOfBirth.SelectedDate = null;
            LoadRoles();
        }

        private void navToAdminWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
    }
}
