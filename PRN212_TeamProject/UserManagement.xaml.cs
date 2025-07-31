using BLL.Services;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class UserManagement : UserControl
    {
        public UserManagement()
        {
            InitializeComponent();
            UserContentControl.Content = new SearchClientControl();
        }

        private void SearchClient_Click(object sender, RoutedEventArgs e)
        {
            UserContentControl.Content = new SearchClientControl();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            UserContentControl.Content = new AddClientControl();
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (UserContentControl.Content is SearchClientControl searchControl)
            {
                var selectedUser = searchControl.SelectedUser;
                if (selectedUser == null)
                {
                    MessageBox.Show("Please select a user to update.", "Warning", MessageBoxButton.OK);
                    return;
                }

                UserContentControl.Content = new UpdateClient(selectedUser);
            }
            else
            {
                MessageBox.Show("You must search and select a user first.", "Warning", MessageBoxButton.OK);
            }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (UserContentControl.Content is SearchClientControl searchControl)
            {
                var selectedUser = searchControl.SelectedUser;
                if (selectedUser == null)
                {
                    MessageBox.Show("Please select a user to delete.", "Warning", MessageBoxButton.OK);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure to delete user: {selectedUser.FullName}?",
                                              "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        var service = new UserService();
                        service.DeleteUser(selectedUser);
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK);
                        searchControl.ReloadUsersAfterDelete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must search and select a user first.", "Warning", MessageBoxButton.OK);
            }
        }
    }
}
