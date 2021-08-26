using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models
{
    public class ProductOrdersUserOrders
    {
        public int Id { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public int ProductOrderId { get; set; }
        public UserOrder UserOrder { get; set; }
        public int UserOrderId { get; set; }
    }
}
