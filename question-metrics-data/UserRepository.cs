using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_data
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public UserRepository()
        {
            _users.Add(new User(
                "Rafael Miceli", 
                "123456", 
                "rafael.miceli@hotmail.com", 
                new DateTime(1989, 12, 07),
                new Guid().ToString())
                    );
        }

        public async Task<User> FindByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> FindById(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<string> Insert(User newUser)
        {
            _users.Add(newUser);
            return newUser.Id;
        }
    }
}