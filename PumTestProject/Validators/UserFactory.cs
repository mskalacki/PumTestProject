using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Validators
{
    public class UserFactory
    {
        public List<User> GetUsers()
        {

            List<User> Users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "SomeUser",
                    Password = "1234"
                },

                new User()
                {
                     Id = 2,
                     UserName = "SomeOtherUser",
                     Password = "4321"
            }
            };
            return Users;
        }
    }
}
