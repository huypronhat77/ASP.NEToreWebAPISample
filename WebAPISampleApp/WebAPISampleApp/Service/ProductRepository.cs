using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;
using WebAPISampleApp.Model;
using WebAPISampleApp.CommonMethod;
using static WebAPISampleApp.CommonMethod.Common;

namespace WebAPISampleApp.Service
{
    public class ProductRepository : IProductRepository
    {
        private MyDbContext _context;
        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public ProductVM Add(ProductModel model)
        {
            var product = new Product()
            {
                ProdId = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                SalePercent = model.SalePercent,
                CatId = model?.CatId,
                Category = _context.categories.FirstOrDefault(cat => cat.CatId.Equals(model.CatId))
            };

            _context.products.Add(product);
            _context.SaveChanges();

            return new ProductVM()
            {
                ProdId = product.ProdId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SalePercent = product.SalePercent,
                category = product.Category.Name
            };
        }

        public List<ProductVM> GetAll()
        {
            return _context.products.Select(x => new ProductVM()
            {
                ProdId = x.ProdId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                SalePercent = x.SalePercent,
                category = x.Category.Name
            }).ToList();
        }

        public List<ProductVM> GetAll(ProductFilterOption filterOptions, ProducSorting sorting)
        {
            // Use AsQueryAble so that the list will filter at the Database
            // instead of getting all record and query on our sever 
            var result = _context.products.Include(cat => cat.Category).AsQueryable();

            #region Filering
            result = Common.FilterProduct(result, filterOptions);
            #endregion Filtering

            #region Sorting
            result = Common.SortProduct(result, sorting);
            #endregion Sorting

            return result.Select(x => new ProductVM()
            {
                ProdId = x.ProdId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                SalePercent = x.SalePercent,
                category = x.Category.Name
            }).ToList();
        }

        public ProductVM GetById(string id)
        {
            var selectedProd = _context.products.Include(p => p.Category).FirstOrDefault(x => x.ProdId.Equals(Guid.Parse(id)));

            if (selectedProd is null)
            {
                return null;
            }

            return new ProductVM()
            {
                ProdId = selectedProd.ProdId,
                Name = selectedProd.Name,
                Description = selectedProd.Description,
                Price = selectedProd.Price,
                SalePercent = selectedProd.SalePercent,
                category = selectedProd.Category.Name
            };
        }

        public bool Remove(string id)
        {
            var isSuccess = false;
            var selectedProd = _context.products.SingleOrDefault(x => x.ProdId.Equals(Guid.Parse(id)));

            if (selectedProd is null)
            {
                return isSuccess;
            }

            _context.products.Remove(selectedProd);
            _context.SaveChanges();

            isSuccess = true;
            return isSuccess;
        }

        public bool Update(string id, ProductModel model)
        {
            var isSuccess = false;
            var selectedProd = _context.products.SingleOrDefault(x => x.ProdId.Equals(Guid.Parse(id)));

            if (selectedProd is null)
            {
                return isSuccess;
            }

            selectedProd.Name = Common.InitializeValidData(selectedProd.Name, model.Name);
            selectedProd.Description = Common.InitializeValidData(selectedProd.Description, model.Description);
            selectedProd.Price = Common.InitializeValidData(selectedProd.Price, model.Price);
            selectedProd.SalePercent = Common.InitializeValidData(selectedProd.SalePercent, model.SalePercent);
            selectedProd.CatId = Common.InitializeValidData(selectedProd.CatId, model.CatId); ;
            selectedProd.Category = _context.categories.FirstOrDefault(cat => cat.CatId.Equals(selectedProd.CatId));

            _context.SaveChanges();

            isSuccess = true;
            return isSuccess;
        }
    }
}
