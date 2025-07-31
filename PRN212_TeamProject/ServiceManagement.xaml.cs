using BLL.Services;
using DAL.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class ServiceManagement : UserControl
    {
        private readonly ServiceService _serviceService = new ServiceService();

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
            if (ServiceContentControl.Content is ViewServiceControl viewControl)
            {
                var selectedService = viewControl.SelectedService;
                if (selectedService == null)
                {
                    MessageBox.Show("Please select a service to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ServiceContentControl.Content = new UpdateServiceControl(selectedService);
            }
            else
            {
                MessageBox.Show("Please switch to View Service tab first and select a service.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceContentControl.Content is ViewServiceControl viewControl)
            {
                var selectedService = viewControl.SelectedService;
                if (selectedService == null)
                {
                    MessageBox.Show("Please select a service to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure to delete service: {selectedService.Name}?",
                                              "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        _serviceService.DeleteService(selectedService);
                        MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        viewControl.LoadService();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please switch to View Service tab first and select a service.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
