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
    /// Interaction logic for ServiceManagement.xaml
    /// </summary>
    public partial class ServiceManagement : UserControl
    {
        public ServiceManagement()
        {
            InitializeComponent();
            ServiceContentControl.Content = new ViewServiceControl();

        }

        private void ViewService_Click(object sender, RoutedEventArgs e)
        {
            ServiceContentControl.Content = new ViewServiceControl();
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            ServiceContentControl.Content = new AddServiceControl();

        }

        private void UpdateService_Click(object sender, RoutedEventArgs e)
        {
            ServiceContentControl.Content = new UpdateServiceControl();

        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
