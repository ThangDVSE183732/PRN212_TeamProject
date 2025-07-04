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
    /// Interaction logic for ServiceTestingWindow.xaml
    /// </summary>
    public partial class ServiceTestingWindow : Window
    {
        private readonly ServiceService _serviceService;

        public ServiceTestingWindow()
        {
            InitializeComponent();
            this._serviceService = new ServiceService();
            LoadService();
        }

        public void LoadService()
        {
            dgService.ItemsSource = _serviceService.GetServices();
        }

        private void dgService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgService.SelectedItem is Service service)
            {
                txtName.Text = service.Name;
                txtDescription.Text = service.Description;
                txtPrice.Text = service.Price.ToString();

                // Set ComboBox based on string value
                foreach (ComboBoxItem item in cbStatus.Items)
                {
                    if (item.Content.ToString().Equals(service.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        cbStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void btn_CreateService_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatusItem = cbStatus.SelectedItem as ComboBoxItem;
            string status = selectedStatusItem?.Content.ToString() ?? "Inactive";

            Service service = new Service()
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = decimal.Parse(txtPrice.Text),
                Status = status,
            };
            _serviceService.CreateService(service);
            LoadService();
        }

        private void btn_UpdateService_Click(object sender, RoutedEventArgs e)
        {
            if (dgService.SelectedItem is Service selectedService)
            {
                var selectedStatusItem = cbStatus.SelectedItem as ComboBoxItem;
                string status = selectedStatusItem?.Content.ToString() ?? "Inactive";

                Service service = new Service()
                {
                    ServiceId = selectedService.ServiceId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Status = status
                };

                _serviceService.UpdateService(service);
                LoadService();
            } else
            {
                MessageBox.Show("Please select a service to continue!");
            }
        }

        private void btn_SearchSerive_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;

            var result = _serviceService.SearchServices(searchText);
            if (result == null)
            {
                MessageBox.Show("No service found!");
            }
            else
            {
                dgService.ItemsSource = result;
            }
        }

        private void btn_DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (dgService.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show("Are you sure you want to deactivate this service?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No) return;

                selectedService.Status = "Inactive";

                _serviceService.UpdateService(selectedService);
                LoadService();
            }
            else
            {
                MessageBox.Show("Please select a service to continue!");
            }
        }
    }
}
