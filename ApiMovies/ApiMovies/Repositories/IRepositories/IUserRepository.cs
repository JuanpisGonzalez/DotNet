using ApiMovies.Models;
using ApiMovies.Models.DTOs;

namespace ApiMovies.Repositories.IRepositories
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool IsUniqueUser(string name);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);
        Task<UserDataDTO> Register(UserDataDTO userDataDTO);
        bool Save();
    }
}
