using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace PRN212_TeamProject
{
    /// <summary>
    /// Interaction logic for BookingServiceControl.xaml
    /// </summary>
    public partial class BookingServiceControl : UserControl
    {
        private ServiceService serviceService;
        private SlotService slotService;
        private BookingService bookingService;
        private Patient _patient;
        public BookingServiceControl(Patient patient)
        {
            InitializeComponent();
            serviceService = new ServiceService();
            slotService = new SlotService();
            bookingService = new BookingService();
            _patient = patient;
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
            try
            {

                if (string.IsNullOrWhiteSpace(txtNote.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtResult.Text) ||
                txtDate.SelectedDate == null ||
                cnbService.SelectedValue == null ||
                cnbSlot.SelectedValue == null)
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

           
                Booking newBooking = new Booking();
                newBooking.Note = txtNote.Text;
                newBooking.Result = txtResult.Text;
                newBooking.Status = "Active";
                newBooking.DateBooking = txtDate.SelectedDate ?? DateTime.Today;
                newBooking.SlotId = int.Parse(cnbSlot.SelectedValue.ToString());
                newBooking.ServiceId = int.Parse(cnbSlot.SelectedValue.ToString());
                newBooking.PatientId = _patient.PatientId;
                if (bookingService.CreateBooking(newBooking))
                {
                    MessageBox.Show("Booking created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed to create booking. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BookingServiceLoader(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }

        private void dgBookingService_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = dgBookingService.SelectedValue as dynamic;
            if (selected != null)
            {
                int id = int.Parse(selected.ServiceId.ToString());
                Service service = serviceService.GetService(id);
                if (service != null)
                {
                    txtPrice.Text = service.Price.ToString();
                    cnbService.SelectedValue = service.ServiceId;
                }
            }
        }
    }
}
