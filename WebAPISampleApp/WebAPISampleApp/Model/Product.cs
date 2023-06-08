using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Model
{
    public class ProductVM
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Product : ProductVM
    {
        public Guid Id { get; set; }
    }
}
