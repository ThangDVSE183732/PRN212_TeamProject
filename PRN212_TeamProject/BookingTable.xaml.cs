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
    /// Interaction logic for BookingTable.xaml
    /// </summary>
    public partial class BookingTable : UserControl
    {
        private readonly BookingService _bookingService = new BookingService();
        public static Booking? SelectedBooking { get; private set; }


        public BookingTable()
        {
            InitializeComponent();
        }

        public Booking? GetSelectedBooking()
        {
            return dgBooking.SelectedItem as Booking;
        }


        public void LoadBookings(List<Booking> bookings)
        {
            dgBooking.ItemsSource = bookings;
        }

        public void SetBookingList(List<Booking> bookings)
        {
            dgBooking.ItemsSource = null;
            dgBooking.ItemsSource = bookings;
        }

        private void dgBooking_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBooking.SelectedItem is Booking booking)
            {
                SelectedBooking = booking;
            }
        }
    }
}
