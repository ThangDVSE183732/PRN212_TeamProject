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
    /// Interaction logic for AddServiceControl.xaml
    /// </summary>
    public partial class AddServiceControl : UserControl
    {
        public AddServiceControl()
        {
            InitializeComponent();
        }

        private readonly ServiceService _serviceService = new ServiceService();

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ UI
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

            // Validate Description (optional)
            if (description.Length > 500)
            {
                MessageBox.Show("Description must be less than or equal to 500 characters.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Price
            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Price cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || price < 0 || price > 99999999.99m)
            {
                MessageBox.Show("Price must be a positive number and less than 100 million.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Kiểm tra trùng tên
            if (_serviceService.ExistByServiceName(name))
            {
                MessageBox.Show("Service name already exists!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Tạo object và gọi service
            Service service = new Service()
            {
                Name = name,
                Description = description,
                Price = price,
                Status = status.ToUpper(), // ACTIVE hoặc INACTIVE
            };

            try
            {
                _serviceService.CreateService(service);
                MessageBox.Show("Service created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            txtServiceName.Text = "";
            txtServiceDescription.Text = "";
            txtServicePrice.Text = "";
            cbServiceStatus.SelectedIndex = 0;
        }

    }
}
