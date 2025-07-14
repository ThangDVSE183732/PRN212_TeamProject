using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepo
    {
        private Prn212Context _context;

        public RoleRepo()
        {
            _context = new Prn212Context();
        }

        public List<Role> GetRoles()
        {
           return _context.Roles.ToList();
        }

    }
}
