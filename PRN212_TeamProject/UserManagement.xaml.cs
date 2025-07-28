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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
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
            UserContentControl.Content = new UpdateClient();

        }

        private void btnDeleteCusomter_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void dgUser_SelectedChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
