using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.Service
{
    public interface ICategoryRepository
    {
        public CategoryVM GetCategory(int Id);
        public List<CategoryVM> GetCategories();
        public CategoryVM AddCategory(CategoryModel category);
        public bool UpdateCategory(int id, CategoryModel model);
        public bool DeleteCategory(int id);
    }
}
