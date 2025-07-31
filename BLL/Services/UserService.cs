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

        public bool ExistsByEmail (string email)
        {
            return _userRepo.ExistsByEmail(email);
        }

        public bool ExistsByPhone (string phone)
        {
            return _userRepo.ExistsByPhone(phone);
        }

        public void CreateUser(User user)
        {
             _userRepo.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            _userRepo.DeleteUser(user);
        }

        public User? GetUserAccount( string email, string password )
        {
            return _userRepo.GetUserAccount(email, password);
        }

        public List<User> GetUsers()
        {
            return _userRepo.GetUsers();
        }

        public bool UpdateUser(User user)
        {
            return _userRepo.UpdateUser(user);
        }

        public bool ExistsByEmailExceptId(string email, int id)
        {
            return _userRepo.ExistsByEmailExceptId(email, id);
        }

        public bool ExistsByPhoneExceptId(string phone, int id)
        {
            return _userRepo.ExistsByPhoneExceptId(phone, id);
        }

    }
}
