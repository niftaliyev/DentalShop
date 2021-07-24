using DentalShop.Areas.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Image> Images { get; set; }
    }
}
