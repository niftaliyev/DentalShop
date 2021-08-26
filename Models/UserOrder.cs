using DentalShop.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models
{
    public class UserOrder
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public IEnumerable<ProductOrdersUserOrders> ProductOrdersUserOrders { get; set; }
    }
}
