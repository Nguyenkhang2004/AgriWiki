using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriWiki_Project.Models;

namespace AgriWiki_Project.Business
{
    public class UserBusiness
    {
        public async Task<User> Create(string email)
        {
            if (email == "khang@gmail.com")
            {
                throw new Exception("Email is already existed");
                // return null;
            }
            return new User();
        }

        public async Task Login(string email, string password)
        {
            if (!(email == "khang@gmail.com" && password == "123"))
            {
                throw new Exception("Incorrect email or password");
                // return null;
            }
        }
    }
}