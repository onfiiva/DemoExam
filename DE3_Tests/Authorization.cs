using DE3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE3_Tests
{
    internal class Authorization
    {

        private List<Users> testUsers = new List<Users>()
        {
            new Users { username = "user", password = "password1", role_name = "USER" },
            new Users { username = "admin", password = "password2", role_name = "ADMIN" }
        };

        public bool Authenticate(string login, string password) 
        {
            //Имитация подключения к БД
            var authQuery = testUsers.FirstOrDefault(user => user.username == login && user.password == password);

            if (authQuery != null && (authQuery.role_name == "USER" || authQuery.role_name == "ADMIN"))
            {
                return true;
            }

            return false;
        } 
    }
}
