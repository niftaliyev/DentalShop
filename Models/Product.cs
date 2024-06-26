﻿using DentalShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string Color { get; set; }
        public string Link { get; set; }
        public Currency Currency { get; set; }
        public IEnumerable<ProductOrder> ProductOrders { get; set; }
        public List<Image> Images { get; set; }
       

    }
}
