using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Data
{

    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public Guid ProdId { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }

        // Relationship
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
