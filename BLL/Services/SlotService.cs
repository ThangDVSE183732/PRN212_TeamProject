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
    public class SlotService
    {
        private SlotRepository _repo;
        public SlotService()
        {
            _repo = new SlotRepository();
        }

        public List<Slot> GetSlot()
        {
            return _repo.GetSlot();
        }

        public bool CreateSlot(Slot slot)
        {
            return _repo.CreateSlot(slot);
        }

        public bool DeleteShift(int id)
        {
           return _repo.DeleteShift(id);
        }


        public Slot GetSlottById(int id)
        {
            return _repo.GetSlottById(id);
        }


        public bool UpdateSlot(Slot slot)
        {
           return _repo.UpdateSlot(slot);
        }

        public List<Slot> GetSlottByShiftId(int id)
        {
            return _repo.GetSlottByShiftId(id);
        }
    }
}
