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
    /// Interaction logic for ServiceTable.xaml
    /// </summary>
    public partial class ServiceTable : UserControl
    {
        private readonly ServiceService _serviceService = new ServiceService();

        public ServiceTable()
        {
            InitializeComponent();
            LoadServiceData();
        }

        private void LoadServiceData()
        {
            var services = _serviceService.GetServices();
            dgService.ItemsSource = services;
        }

        public Service? SelectedService => dgService.SelectedItem as Service;

        public void SetServices(List<Service> services)
        {
            dgService.ItemsSource = null;
            dgService.ItemsSource = services;
        }
    }
}
