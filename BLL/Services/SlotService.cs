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
    }
}
