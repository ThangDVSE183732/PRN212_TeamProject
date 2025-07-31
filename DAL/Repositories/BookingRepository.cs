using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class BookingRepository
    {
        private readonly Prn212Context _context;

        public BookingRepository()
        {
            _context = new Prn212Context();
        }

        // find by ServiceName, PatientName, DoctorName
        public List<Booking> SearchBookings(string keyword)
        {
            keyword = keyword?.Trim().ToLower() ?? "";

            return _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Patient)
                .Include(b => b.Slot)
                    .ThenInclude(s => s.Shift)
                        .ThenInclude(sh => sh.Doctor)
                .Where(b =>
                    b.Service.Name.ToLower().Contains(keyword) ||
                    b.Patient.Name.ToLower().Contains(keyword) ||
                    b.Slot.Shift.Doctor.DoctorName.ToLower().Contains(keyword))
                .AsNoTracking()
                .ToList();
        }


        // Get all bookings
        public List<Booking> GetAll()
        {
            return _context.Bookings
                .Include(b => b.Patient)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                    .ThenInclude(s => s.Shift)
                        .ThenInclude(sh => sh.Doctor)
                .AsNoTracking()
                .ToList();
        }


        // Get booking by ID
        public Booking? GetById(int id)
        {
            return _context.Bookings
                .Include(b => b.Patient)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .FirstOrDefault(b => b.BookingId == id);
        }

        // Get bookings by Patient ID
        public List<Booking> GetByPatientId(int patientId)
        {
            return _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Where(b => b.PatientId == patientId)
                .AsNoTracking()
                .ToList();
        }

        // Get bookings by status
        public List<Booking> GetByStatus(string status)
        {
            return _context.Bookings
                .Include(b => b.Patient)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Where(b => b.Status.ToUpper() == status.ToUpper())
                .ToList();
        }

        // Get bookings by Service ID
        public List<Booking> GetByServiceId(int serviceId)
        {
            return _context.Bookings
                .Include(b => b.Patient)
                .Include(b => b.Slot)
                .Where(b => b.ServiceId == serviceId)
                .ToList();
        }

        // Get today's bookings
        public List<Booking> GetToday()
        {
            DateTime today = DateTime.Today;
            return _context.Bookings
                .Include(b => b.Patient)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Where(b => b.DateBooking.Date == today)
                .ToList();
        }

        // Create booking
        public void Create(Booking booking)
        {
            booking.BookingId = GetNextId();
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        // Update booking
        public bool UpdateBooking(Booking updatedBooking)
        {
            var existing = _context.Bookings.FirstOrDefault(b => b.BookingId == updatedBooking.BookingId);
            if (existing == null)
                return false;

            // Cập nhật các trường
            existing.PatientId = updatedBooking.PatientId;
            existing.ServiceId = updatedBooking.ServiceId;
            existing.SlotId = updatedBooking.SlotId;
            existing.DateBooking = updatedBooking.DateBooking;
            existing.Note = updatedBooking.Note;
            existing.Result = updatedBooking.Result;
            existing.Status = updatedBooking.Status;

            return _context.SaveChanges() > 0;
        }


        // Delete booking
        public bool Delete(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            return _context.SaveChanges() > 0;
        }


        // Check if a slot is already booked on a specific date
        public bool CheckSlotAvailability(int slotId, DateTime date)
        {
            return !_context.Bookings
                .Any(b => b.SlotId == slotId && b.DateBooking.Date == date.Date);
        }

        // Get next ID
        public int GetNextId()
        {
            return (_context.Bookings.Max(b => (int?)b.BookingId) ?? 0) + 1;
        }

        public bool IsSlotTaken(int slotId, DateTime dateBooking, int currentBookingId)
        {
            return _context.Bookings.Any(b =>
                b.SlotId == slotId
                && b.DateBooking.Date == dateBooking.Date
                && b.BookingId != currentBookingId);
        }
    }
}
