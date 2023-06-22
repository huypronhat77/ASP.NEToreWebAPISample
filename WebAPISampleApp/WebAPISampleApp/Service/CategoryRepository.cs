using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.Service
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyDbContext _context;

        public CategoryRepository(MyDbContext context)
        {
            _context = context;
        }
        public CategoryVM AddCategory(CategoryModel category)
        {
            var newCat = new Category()
            {
                Name = category.Name
            };

            _context.categories.Add(newCat);
            _context.SaveChanges();

            return new CategoryVM()
            {
                CatId = newCat.CatId,
                Name = newCat.Name              
            };
        }

        public bool DeleteCategory(int id)
        {
            var isSuccess = false;
            var category = _context.categories.SingleOrDefault(x => x.CatId.Equals(id));

            if (category != null)
            {
                _context.categories.Remove(category);
                _context.SaveChanges();
                isSuccess = true;
            }

            return isSuccess;
        }

        public List<CategoryVM> GetCategories()
        {
            var categories = _context.categories.Select(x => new CategoryVM
            {
                CatId = x.CatId,
                Name = x.Name
            });

            return categories.ToList();
        }

        public CategoryVM GetCategory(int Id)
        {
            var category = _context.categories.SingleOrDefault(x => x.CatId.Equals(Id));

            if (category != null)
            {
                return new CategoryVM()
                {
                    CatId = category.CatId,
                    Name = category.Name
                };
            }

            return null;
        }

        public bool UpdateCategory(int id, CategoryModel model)
        {
            var isSuccess = false;
            var category = _context.categories.SingleOrDefault(x => x.CatId.Equals(id));

            if (category != null)
            {
                category.Name = model.Name;
                _context.SaveChanges();
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
