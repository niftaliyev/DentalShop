using DentalShop.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.ViewModels
{
    public class OrderUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public AppUser AppUserModel { get; set; }
    }
}
