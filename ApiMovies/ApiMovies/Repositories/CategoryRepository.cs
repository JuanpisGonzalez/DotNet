using ApiMovies.Data;
using ApiMovies.Models;
using ApiMovies.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateCategory(Category category)
        {
            category.CreatedDate = DateTime.Now;
            _context.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return Save();
        }

        public bool ExistCategory(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool ExistCategory(string name)
        {
            bool value = _context.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            category.CreatedDate = DateTime.Now;

            var categoryExist = _context.Categories.Find(category.Id);
            if (categoryExist != null)
            {
                _context.Entry(categoryExist).CurrentValues.SetValues(category);//Update category that is being tracking

            }
            else
            {
                _context.Categories.Update(category);
            }

            return Save();
        }
    }
}
