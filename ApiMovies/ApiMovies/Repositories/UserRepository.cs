using ApiMovies.Data;
using ApiMovies.Models;
using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;

namespace ApiMovies.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsUniqueUser(string name)
        {
            return _context.Users.Any(x => x.Name == name);
        }

        public Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataDTO> Register(UserDataDTO userDataDTO)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
