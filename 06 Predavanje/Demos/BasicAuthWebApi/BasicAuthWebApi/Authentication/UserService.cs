using System.Linq;
using System.Collections.Generic;

namespace BasicAuthWebApi.Authentication
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "user", Role=Role.User, Password = "user" },
            new User { Id = 1, Username = "admin", Role=Role.Admin, Password = "admin" },
        };

        public User Authenticate(string username, string password)
        {
            return _users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
