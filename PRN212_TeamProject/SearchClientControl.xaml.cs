using BLL.Services;
using DAL.Entities;
using Microsoft.VisualBasic.ApplicationServices;
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
    /// Interaction logic for SearchClientControl.xaml
    /// </summary>
    public partial class SearchClientControl : UserControl
    {
        private UserService _userService = new UserService();

        public DAL.Entities.User? SelectedUser => userTableControl.dgUser.SelectedItem as DAL.Entities.User;

        public SearchClientControl()
        {
            InitializeComponent();
        }

        public void ReloadUsersAfterDelete()
        {
            var users = _userService.GetUsers();
            userTableControl.SetUsers(users);
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var results = _userService.GetUsers()
                .Where(x => x.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            x.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Any())
            {
                userTableControl.SetUsers(results);
                userTableControl.Visibility = Visibility.Visible;
            }
            else
            {
                userTableControl.Visibility = Visibility.Collapsed;
                MessageBox.Show("No users found.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public DAL.Entities.User? GetSelectedUser()
        {
            return userTableControl.GetSelectedUser();
        }


        //private void dgUser_SelectedChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
