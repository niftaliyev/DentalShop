﻿using DentalShop.Areas.Admin.Model;
using DentalShop.Models;
using DentalShop.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly DentalShopDbContext _context;

        public OrdersController(DentalShopDbContext context)
        {
            _context = context;
        }

        [HttpPost("buy")]
        [Authorize]
        public async Task<ActionResult> Buy(BuyDTO order)
        {
            var username = User.Identity.Name.ToString();
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);

            var userOrders = _context.ProductOrders.Where(x => x.AppUserId == user.Id);



            foreach (var itemOrder in order.Orders)
            {
                var product = _context.Products.Where(x => x.Id == itemOrder.ProductId).FirstOrDefault();
                var test = userOrders.Any(x => x.ProductId == itemOrder.ProductId && x.Product.Color == product.Color && x.Delivery != Delivery.CATDIRILDI);

                var count = order.Orders.Where(x => x.ProductId == itemOrder.ProductId).Count();

                if (test)
                {
                    var itemUserOrder = _context.ProductOrders.Where(x => x.AppUserId == user.Id && x.ProductId == itemOrder.ProductId && x.Delivery == Delivery.YENI).FirstOrDefault();
                    itemUserOrder.Count += itemOrder.Count;
                    _context.ProductOrders.Update(itemUserOrder);
                }

                else if (count < 2)
                {

                    _context.ProductOrders.Add(new ProductOrder
                    {
                        ProductId = itemOrder.ProductId,
                        Count = itemOrder.Count,
                        AppUserId = user.Id
                    });
                }




            }
            _context.SaveChanges();



            return Ok();

        }


    }


}
