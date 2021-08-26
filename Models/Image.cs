using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Model
{
    //[Table(name: "images")]
    public class Image
    {
        //[Column("id")]
        public int Id { get; set; }

        //[Column("product_image")]
        public string ProductImage { get; set; }

        //[Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
