using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private UserRepo _userRepo;
        public UserService() 
        {
            _userRepo = new UserRepo();
        }
        public User? GetUserAccount( string email, string password )
        {
            return _userRepo.GetUserAccount(email, password);
        } 
    }
}
