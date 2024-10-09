using ApiMovies.Models;
using Microsoft.AspNetCore.Components.Web;

namespace ApiMovies.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        bool ExistCategory(int id);
        bool ExistCategory(string name);
        bool CreateCategory (Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
