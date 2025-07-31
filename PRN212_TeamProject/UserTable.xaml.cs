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
    /// Interaction logic for UserTable.xaml
    /// </summary>
    public partial class UserTable : UserControl
    {
        private readonly UserService _userService = new UserService();

        public UserTable()
        {
            InitializeComponent();
            LoadUserData();
        }

        public void SetUsers(List<User> users)
        {
            dgUser.ItemsSource = null;
            dgUser.ItemsSource = users;
        }

        public User GetSelectedUser()
        {
            return dgUser.SelectedItem as User;
        }


        private void LoadUserData()
        {
            var users = _userService.GetUsers();
            dgUser.ItemsSource = users;
        }

    }
}
