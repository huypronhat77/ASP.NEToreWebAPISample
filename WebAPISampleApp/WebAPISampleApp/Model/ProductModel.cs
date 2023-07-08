using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Model
{
    public class ProductModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }

        public byte SalePercent { get; set; }
        public int? CatId { get; set; }
    }
}
