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
using System.Xml.Linq;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for UserProfileControl.xaml
    /// </summary>
    public partial class UserProfileControl : UserControl
    {
        private User _user;
        private UserService _userService;
        public UserProfileControl(User user)
        {
            InitializeComponent();
            _user = user;
            _userService = new UserService();
        }

        private void UserProfileLoader(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = _user != null ? _user.FullName : "";
            txtPassword.Text = _user != null ? _user.Password : "";
            txtPhone.Text = _user != null ? _user.Phone : "";
            txtEmail.Text = _user != null ? _user.Email : "";
            txtAddress.Text = _user != null ? _user.Address : "";
            txtBD.SelectedDate = _user != null ?
                _user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue) : null;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User updateUser = _userService.GetUserAccount(_user.Email, _user.Password);
                if(updateUser != null)
                {

                    updateUser.FullName = txtFullName.Text;
                    updateUser.Password = txtPassword.Text;
                    updateUser.Email = txtEmail.Text;
                    updateUser.Phone = txtPhone.Text;
                    updateUser.Address = txtAddress.Text;
                    updateUser.DateOfBirth = DateOnly.FromDateTime(txtBD.SelectedDate ?? DateTime.Today);
                }
                if (_userService.UpdateUser(updateUser))
                {
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to updated user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the profile: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
