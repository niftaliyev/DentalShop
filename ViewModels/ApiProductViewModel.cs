using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.ViewModels
{
    public class ApiProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
