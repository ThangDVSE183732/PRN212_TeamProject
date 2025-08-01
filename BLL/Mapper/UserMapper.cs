using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class UserMapper
    {
        public static User ToUser (string email, string password)
        {
            User newUser = new User();
            newUser.Email = email;
            newUser.Password = password;
            newUser.FullName = "";
            newUser.Phone = "";
            newUser.RoleId = 5;
            newUser.Address = "";
            newUser.Status = "Active";
            newUser.DateOfBirth = DateOnly.FromDateTime(DateTime.Now);
            return newUser;
        }

        public static User ToDoctor(string email, string password)
        {
            User newUser = new User();
            newUser.Email = email;
            newUser.Password = password;
            newUser.FullName = "";
            newUser.Phone = "";
            newUser.RoleId = 3;
            newUser.Address = "";
            newUser.Status = "Active";
            newUser.DateOfBirth = DateOnly.FromDateTime(DateTime.Now);
            return newUser;
        }
    }
}
