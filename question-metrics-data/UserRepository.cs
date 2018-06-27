using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using question_metrics_domain;
using question_metrics_domain.Interfaces;

namespace question_metrics_data {
    public class UserRepository : IUserRepository {
        private IMongoDatabase _mongoDb;
        private static IMongoCollection<UserDataDto> _users {get; set;}

        public UserRepository(MongoClient client) {
            _mongoDb = client.GetDatabase("QuestionMetrics");
            _users = _mongoDb.GetCollection<UserDataDto>("users");
        }

        public async Task<User> FindByEmail(string email) {
            return (await _users.Find(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync())?.ToUser();
        }

        public async Task<User> FindById(string id) {
            return _users.Find(_ => true).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> Insert(User newUser) {
            await _users.InsertOneAsync(newUser.ToUserData());
            return newUser.Id;
        }

        public async Task UpdateUser(User userInDatabase) {
            //_users.DeleteOneAsync(u => );
            //_users.Add(userInDatabase);
        }
    }
}