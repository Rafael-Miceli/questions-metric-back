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

        public async Task UpdateUser(User userInDatabase)
        {
            _users.Remove(_users.FirstOrDefault(u => u.Id == userInDatabase.Id));
            _users.Add(userInDatabase);
        }
    }
}