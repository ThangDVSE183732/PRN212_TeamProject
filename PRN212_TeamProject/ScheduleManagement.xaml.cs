using BLL.Services;
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
    /// Interaction logic for ScheduleManagement.xaml
    /// </summary>
    public partial class ScheduleManagement : UserControl
    {
        private readonly BookingService _bookingService = new BookingService();
        public ScheduleManagement()
        {
            InitializeComponent();
            ScheduleContentControl.Content = new ViewSheduleControl();

        }

        private void ViewSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleContentControl.Content = new ViewSheduleControl();
        }

        private void UpdateSchedule_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooking = ViewSheduleControl.GetSelectedBooking();
            if (selectedBooking == null)
            {
                MessageBox.Show("Please select a schedule to update!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ScheduleContentControl.Content = new UpdateScheduleControl(selectedBooking);
        }


        private void DeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleContentControl.Content is ViewSheduleControl viewControl)
            {
                var selectedBooking = viewControl.bookingTableControl.GetSelectedBooking();
                if (selectedBooking == null)
                {
                    MessageBox.Show("Please select a booking to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this booking?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool result = _bookingService.Delete(selectedBooking.BookingId);
                    if (result)
                    {
                        MessageBox.Show("Booking deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        viewControl.LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
