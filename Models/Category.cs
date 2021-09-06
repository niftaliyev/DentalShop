using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
