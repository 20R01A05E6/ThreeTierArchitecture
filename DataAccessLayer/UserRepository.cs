using YourNamespace.Data;
using YourNamespace.Models;

namespace YourNamespace.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsernameAndPassword(string username, string password);
        void AddUser(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        static void Main(string[] args)
        {
            
        }
    }
}
