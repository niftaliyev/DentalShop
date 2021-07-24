using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Model
{
    public enum OrderStatus
    {
        New,
        Delivery,
        Done
    }
    public class Order
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
