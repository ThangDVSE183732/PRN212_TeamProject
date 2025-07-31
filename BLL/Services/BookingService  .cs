using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookingService
    {
        private BookingRepository _repo;
        public BookingService()
        {
            _repo = new BookingRepository();
        }

        public bool CreateBooking(Booking booking)
        {
            return _repo.CreateBooking(booking);
        }

        public Booking GetBookingById(int id)
        {
            return _repo.GetBookingById(id);
        }
    }
}
