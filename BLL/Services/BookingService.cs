using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }


        public bool CreateBooking(Booking booking)
        {
            return _bookingRepository.CreateBooking(booking);
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public List<Booking> SearchBookings(string keyword)
        {
            return _bookingRepository.SearchBookings(keyword);
        }

        public List<Booking> GetAll() => _bookingRepository.GetAll();

        public Booking? GetById(int id) => _bookingRepository.GetById(id);

        public List<Booking> GetByPatientId(int patientId) => _bookingRepository.GetByPatientId(patientId);

        public List<Booking> GetByStatus(string status) => _bookingRepository.GetByStatus(status);

        public List<Booking> GetByServiceId(int serviceId) => _bookingRepository.GetByServiceId(serviceId);

        public List<Booking> GetToday() => _bookingRepository.GetToday();

        public void Create(Booking booking) => _bookingRepository.Create(booking);

        public bool Update(Booking booking) => _bookingRepository.UpdateBooking(booking);

        public bool Delete(int bookingId) => _bookingRepository.Delete(bookingId);

        public bool IsSlotTaken(int selectedValue, DateTime value, int bookingId)
        {
            return _bookingRepository.IsSlotTaken(selectedValue, value, bookingId);
        }
    }
}
