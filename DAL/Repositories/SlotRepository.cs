using DAL.Entities;
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
            return _context.Slots.ToList();
        }

    }
}
