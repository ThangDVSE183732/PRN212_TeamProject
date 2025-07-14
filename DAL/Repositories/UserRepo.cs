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
            return _dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password );
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

        public void UpdateUser(User user)
        {
            User existingUser = _dbContext.Users.Find(user.UserId); // Tìm đúng theo PK
            if (existingUser != null)
            {
                // Chỉ gán giá trị từng field, không tạo mới instance
                existingUser.FullName = user.FullName;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.RoleId = user.RoleId;
                existingUser.Password = user.Password;
                existingUser.Status = user.Status;
                _dbContext.SaveChanges();
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

    }
}
