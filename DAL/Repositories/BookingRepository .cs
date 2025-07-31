using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookingRepository
    {
        private Prn212Context _context;

        public BookingRepository()
        {
            _context = new Prn212Context();
        }


        public bool CreateBooking(Booking booking)
        {
            bool isSuccess = false;
            var existedBooking = GetBookingById(booking.BookingId);
            if (existedBooking == null)
            {
                int maxId = _context.Bookings.Any()
                   ? _context.Bookings.Max(x => x.BookingId)
                   : 1;
                booking.BookingId = maxId + 1;
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.Find(id);
        }
    }
}
