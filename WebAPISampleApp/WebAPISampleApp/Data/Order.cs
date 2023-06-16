using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISampleApp.Data
{
    public enum OrderStatus
    {
        Scheduled,
        OnTheWay,
        Finished
    }
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public OrderStatus Status { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ReceiverName { get; set; }

        // Relationship
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            OrderDate = DateTime.Now;
        }
    }
}
