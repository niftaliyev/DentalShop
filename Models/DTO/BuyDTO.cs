using DentalShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models.DTO
{
    public class BuyDTO
    {
        public List<BuyProductsViewModel> Orders { get; set; }
    }
}
