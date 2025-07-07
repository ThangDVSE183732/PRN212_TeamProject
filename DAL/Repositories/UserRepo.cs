using DAL.Entities;
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

        public User? GetUserAccount(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password );
        }

    }
}
