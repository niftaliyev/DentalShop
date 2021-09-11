using DentalShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.ViewModels
{
    public class DetailOrdersViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public IEnumerable<ProductOrder> ProductOrders { get; set; }
    }
}
