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
    /// Interaction logic for ViewServiceControl.xaml
    /// </summary>
    public partial class ViewServiceControl : UserControl
    {
        private readonly ServiceService _serviceService;

        public Service SelectedService => dgService.SelectedItem as Service;

        public ViewServiceControl()
        {
            InitializeComponent();
            _serviceService = new ServiceService();
            LoadService();
        }

        public void LoadService()
        {
            List<Service> services = _serviceService.GetServices();
            dgService.ItemsSource = services;
        }
        private void dgService_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var results = _serviceService.GetServices()
                .Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            x.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            dgService.ItemsSource = results;
        }
    }
}
