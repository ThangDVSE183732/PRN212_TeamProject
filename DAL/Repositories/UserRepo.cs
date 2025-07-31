using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepo
    {
        private Prn212Context _dbContext;

        public UserRepo()
        {
            _dbContext = new Prn212Context();
        }

        public void CreateUser(User user)
        {
            int maxId = _dbContext.Users.Any()
                   ? _dbContext.Users.Max(x => x.UserId)
                   : 1;
            user.UserId = maxId + 1;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
           _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User? GetUserAccount(string email, string password)
        {
            return _dbContext.Users.Include(u => u.Role).FirstOrDefault(x => x.Email == email && x.Password == password );
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.Include(x=>x.Role).ToList();
        }

        public bool ExistsByEmail(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }

        public bool ExistsByPhone(string phone)
        {
            return _dbContext.Users.Any(u => u.Phone == phone);
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var existing = _dbContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (existing == null) return false;

                existing.FullName = user.FullName;
                existing.Email = user.Email;
                existing.Phone = user.Phone;
                existing.Address = user.Address;
                existing.Password = user.Password;
                existing.DateOfBirth = user.DateOfBirth;
                existing.Status = user.Status;
                existing.RoleId = user.RoleId;

                int changed = _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool ExistsByEmailExceptId(string email, int id)
        {
            return _dbContext.Users.Any(u => u.Email == email && u.UserId != id);
        }

        public bool ExistsByPhoneExceptId(string phone, int id)
        {
            return _dbContext.Users.Any(u => u.Phone == phone && u.UserId != id);
        }

        public User GetUserById(int id) 
        {
            return _dbContext.Users.Find(id);
        }

        
    }
}
