using BLL.Services;
using DAL.Entities;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

            }
        }

        private void btn_CreateService_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Service name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (name.Length > 100)
            {
                MessageBox.Show("Service name must be less than or equal to 100 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Description (optional)
            string description = txtDescription.Text.Trim();
            if (description.Length > 500)
            {
                MessageBox.Show("Description must be less than or equal to 500 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Price
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Price cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            decimal price = 0;
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Price must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_serviceService.ExistByServiceName(name))
            {
                MessageBox.Show("Service name already exists!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            Service service = new Service()
            {
                Name = name,
                Description = description,
                Price = price,
                Status = "ACTIVE",
            };
            _serviceService.CreateService(service);
            LoadService();
        }

        private void btn_UpdateService_Click(object sender, RoutedEventArgs e)
        {
            if (dgService.SelectedItem is Service selectedService)
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Service name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (name.Length > 100)
                {
                    MessageBox.Show("Service name must be less than or equal to 100 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate Description (optional)
                string description = txtDescription.Text.Trim();
                if (description.Length > 500)
                {
                    MessageBox.Show("Description must be less than or equal to 500 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate Price
                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Price cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                decimal price = 0;
                if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
                {
                    MessageBox.Show("Price must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(_serviceService.ExistsByServiceNameExceptId(name, selectedService.ServiceId))
                {
                    MessageBox.Show("Service name already exists!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Service service = new Service()
                {
                    ServiceId = selectedService.ServiceId,
                    Name = name,
                    Description = description,
                    Price = price,
                    Status = "ACTIVE"
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

        private void btn_InactiveService_Click(object sender, RoutedEventArgs e)
        {
            if (dgService.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show("Are you sure you want to deactivate this service?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No) return;

                selectedService.Status = "INACTIVE";

                _serviceService.UpdateService(selectedService);
                LoadService();
            }
            else
            {
                MessageBox.Show("Please select a service to continue!");
            }
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void btn_ActiveService_Click(object sender, RoutedEventArgs e)
        {
            if (dgService.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show("Are you sure you want to active this service?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No) return;

                selectedService.Status = "ACTIVE";

                _serviceService.UpdateService(selectedService);
                LoadService();
            }
            else
            {
                MessageBox.Show("Please select a service to continue!");
            }
        }

        private void btn_ClearChoice_Click(object sender, RoutedEventArgs e)
        {
            dgService.UnselectAll();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
        }
    }
}
