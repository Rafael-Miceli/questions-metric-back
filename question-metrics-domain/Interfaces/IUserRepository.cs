using System.Threading.Tasks;

namespace question_metrics_domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmail(string email);
        Task<string> Insert(User newUser);
        Task<User> FindById(string id);
        Task UpdateUser(User userInDatabase);
    }
}