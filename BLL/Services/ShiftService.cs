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
    public class ShiftService
    {
        private ShiftRepository _repo;
        public ShiftService()
        {
            _repo = new ShiftRepository();
        }

        public bool CreateShift(Shift shift)
        {
            return _repo.CreateShift(shift);
        }

        public bool DeleteShift(int id)
        {
            return _repo.DeleteShift(id);
        }

        public List<Shift> GetAll()
        {
            return _repo.GetAll();
        }


        public Shift GetShiftById(int id)
        {
            return _repo.GetShiftById(id);
        }

        public bool UpdateShift(Shift shift)
        {
            return _repo.UpdateShift(shift);
        }
    }
}
