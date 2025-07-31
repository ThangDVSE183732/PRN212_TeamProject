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
    /// Interaction logic for BookingServiceControl.xaml
    /// </summary>
    public partial class BookingServiceControl : UserControl
    {
        private ServiceService serviceService;
        private SlotService slotService;
        public BookingServiceControl()
        {
            InitializeComponent();
            serviceService = new ServiceService();
            slotService = new SlotService(); 
        }

        public void loadDataInit()
        {
            try
            {
                this.dgBookingService.ItemsSource = serviceService.GetServices()
                    .Select(r => new
                    {
                        r.ServiceId,
                        r.Name,
                        r.Description,
                        r.Price,
                        r.Status
                    });
                cnbService.ItemsSource = serviceService.GetServices();
                cnbService.DisplayMemberPath = "Name";
                cnbService.SelectedValuePath = "ServiceId";

                cnbSlot.ItemsSource = slotService.GetSlot();
                cnbSlot.DisplayMemberPath = "DisplayTime";
                cnbSlot.SelectedValuePath = "SlotId";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookingServiceLoader(object sender, RoutedEventArgs e)
        {

        }

        private void dgBookingService_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
