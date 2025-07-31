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

        public BookingTable()
        {
            InitializeComponent();
            LoadBookingData();
        }

        private void LoadBookingData()
        {
            List<Booking> bookings = _bookingService.GetAll();
            dgBooking.ItemsSource = bookings;
        }
    }
}
