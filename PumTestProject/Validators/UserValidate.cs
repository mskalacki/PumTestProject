using PumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumTestProject.Validators
{
    public class UserValidate
    {

        public static bool Login(string username, string password)
        {
            UserFactory factory = new UserFactory();
            List<User> UserLists = factory.GetUsers();
            return UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);

        }
    }
}
