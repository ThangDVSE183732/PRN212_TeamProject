using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ShiftRepository
    {
        private readonly Prn212Context _context;
        public ShiftRepository()
        {
            _context = new Prn212Context();
        }

        public bool CreateShift(Shift shift)
        {
            bool isSuccess = false;
            var existedShift = GetShiftById(shift.ShiftId);
            if (existedShift == null)
            {
                int maxId = _context.Shifts.Any()
                   ? _context.Shifts.Max(x => x.ShiftId)
                   : 1;
                shift.ShiftId = maxId + 1;
                _context.Shifts.Add(shift);
                _context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool DeleteShift(int id)
        {
            bool isSuccess = false;
            var shift = GetShiftById(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                _context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public List<Shift> GetAll()
        {
            return _context.Shifts.Include(r => r.Doctor).AsNoTracking().ToList();
        }


        public Shift GetShiftById(int id)
        {
            return _context.Shifts.Find(id);
        }

        public Shift GetShiftByDoctorId(int id)
        {
            return _context.Shifts.FirstOrDefault(c => c.DoctorId == id);
        }

        public bool UpdateShift(Shift shift)
        {
            bool isSuccess = false;
            var updateShift = GetShiftById(shift.ShiftId);
            if (updateShift != null)
            {
                var tracker = _context.Attach(updateShift);
                tracker.State = EntityState.Modified;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                isSuccess = true;
            }
            return isSuccess;
        }
    }
}
