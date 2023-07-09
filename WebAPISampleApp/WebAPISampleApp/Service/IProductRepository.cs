using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Model;
using static WebAPISampleApp.CommonMethod.Common;

namespace WebAPISampleApp.Service
{
    public interface IProductRepository : IRepository<ProductVM, ProductModel>
    {
        List<ProductVM> GetAll(ProductFilterOption filterOptions, ProducSorting sorting, int page);
    }
}
