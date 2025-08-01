using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRN212_TeamProject
{
    public partial class ViewSheduleControl : UserControl
    {
        private readonly BookingService _bookingService;

        public ViewSheduleControl()
        {
            InitializeComponent();
            _bookingService = new BookingService();
            btnSearch_Click(null, null);
        }

        public void LoadData(string keyword = "")
        {
            var list = string.IsNullOrWhiteSpace(keyword)
                ? _bookingService.GetAll()
                : _bookingService.SearchBookings(keyword);
            bookingTableControl.SetBookingList(list);
        }


        public static Booking? GetSelectedBooking()
        {
            return BookingTable.SelectedBooking;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var result = _bookingService.SearchBookings(keyword);
            bookingTableControl.LoadBookings(result);
        }

        public static Booking? SelectedBooking { get; private set; }

        //private void dgSchedule_SelectedChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (dgSchedule.SelectedItem is Booking booking)
        //    {
        //        SelectedBooking = booking;
        //    }
        //}

    }
}
