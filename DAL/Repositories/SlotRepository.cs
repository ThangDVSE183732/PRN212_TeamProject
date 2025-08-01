using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SlotRepository
    {
        private readonly Prn212Context _context;
        public SlotRepository()
        {
            _context = new Prn212Context();
        }

        public List<Slot> GetSlot()
        {
            return _context.Slots.Include(c => c.Shift).AsNoTracking().ToList();
        }

        public bool CreateSlot(Slot slot)
        {
            bool isSuccess = false;
            var existedSlot = GetSlottById(slot.SlotId);
            if (existedSlot == null)
            {
                int maxId = _context.Slots.Any()
                   ? _context.Slots.Max(x => x.SlotId)
                   : 1;
                slot.SlotId = maxId + 1;
                _context.Slots.Add(slot);
                _context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool DeleteShift(int id)
        {
            bool isSuccess = false;
            var slot = GetSlottById(id);
            if (slot != null)
            {
                _context.Slots.Remove(slot);
                _context.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }


        public Slot GetSlottById(int id)
        {
            return _context.Slots.Find(id);
        }

   
        public bool UpdateSlot(Slot slot)
        {
            bool isSuccess = false;
            var updateSlot = GetSlottById(slot.SlotId);
            if (updateSlot != null)
            {
                var tracker = _context.Attach(updateSlot);
                tracker.State = EntityState.Modified;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                isSuccess = true;
            }
            return isSuccess;
        }

    }
}
