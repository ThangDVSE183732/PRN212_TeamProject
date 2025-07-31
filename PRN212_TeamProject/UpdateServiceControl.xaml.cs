using BLL.Services;
using DAL.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class UpdateServiceControl : UserControl
    {
        private readonly ServiceService _serviceService = new ServiceService();
        private readonly Service _selectedService;

        public UpdateServiceControl(Service service)
        {
            InitializeComponent();
            _selectedService = service;
            LoadService();
        }


        private void LoadService()
        {
            txtServiceId.Text = _selectedService.ServiceId.ToString();
            txtServiceName.Text = _selectedService.Name;
            txtServiceDescription.Text = _selectedService.Description;
            txtServicePrice.Text = _selectedService.Price.ToString();

            if (_selectedService.Status.ToUpper() == "ACTIVE")
            {
                cbServiceStatus.SelectedIndex = 0;
            }
            else
            {
                cbServiceStatus.SelectedIndex = 1;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = txtServiceName.Text.Trim();
            string description = txtServiceDescription.Text.Trim();
            string priceText = txtServicePrice.Text.Trim();
            string status = ((ComboBoxItem)cbServiceStatus.SelectedItem)?.Content?.ToString() ?? "Inactive";

            // Validate Name
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

            // Validate Description
            if (description.Length > 500)
            {
                MessageBox.Show("Description must be less than or equal to 500 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Price
            if (!decimal.TryParse(priceText, out decimal price) || price < 0)
            {
                MessageBox.Show("Price must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check for duplicate name if changed
            if (_selectedService.Name != name && _serviceService.ExistsByServiceNameExceptId(name, _selectedService.ServiceId))
            {
                MessageBox.Show("Service name already exists!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the object
            Service updated = new Service
            {
                ServiceId = _selectedService.ServiceId,
                Name = name,
                Description = description,
                Price = price,
                Status = status.ToUpper()
            };

            try
            {
                _serviceService.UpdateService(updated);
                MessageBox.Show("Service updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
