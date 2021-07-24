using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models
{
    [Table(name: "orderproductuser")]
    public class OrderProductsUsers
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("count")]
        public int Count { get; set; }
    }
}
