using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService
    {

        private RoleRepo _repo;

        public RoleService()
        {
            _repo = new RoleRepo();
        }

        public List<Role> GetRoles()
        {
            return _repo.GetRoles();
        }
    }
}
