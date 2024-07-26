using YourNamespace.Models;
using YourNamespace.Repositories;

namespace YourNamespace.Services
{
    public interface IUserService
    {
        bool Login(string username, string password);
        void Register(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsernameAndPassword(username, password);
            return user != null;
        }

        public void Register(User user)
        {
            _userRepository.AddUser(user);
        }
        static void Main(string[] args)
        {
            
        }
    }
}
