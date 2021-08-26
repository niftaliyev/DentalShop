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
            double price = 0;

            List<ProductOrder> orderProducts = new List<ProductOrder>();

            var checkUserOrder = _context.UserOrders.FirstOrDefault(x => x.AppUserId == user.Id);
            if (checkUserOrder == null)
            {
                foreach (var item in order.Orders)
                {
                    var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);

                    if (product != null)
                    {
                        var existingOrder = orderProducts.FirstOrDefault(x => x.ProductId == product.Id);
                        price += product.Price;

                        if (existingOrder == null)
                        {
                            orderProducts.Add(new ProductOrder { ProductId = item.ProductId, Count = item.Count });

                        }
                        else
                        {
                            existingOrder.Count += item.Count;
                        }
                    }
                }

                if (orderProducts.Count > 0 && price > 20)
                {
                  

                    UserOrder userOrder = new UserOrder {AppUserId = user.Id };
                    _context.UserOrders.Add(userOrder);
                    await _context.SaveChangesAsync();

                    _context.PorductOrders.AddRange(orderProducts);
                    await _context.SaveChangesAsync();

                    foreach (var item in orderProducts)
                    {
                     _context.ProductOrdersUserOrders.AddRange(new ProductOrdersUserOrders { UserOrderId = userOrder.Id, ProductOrderId = item.Id  });
                     await _context.SaveChangesAsync();

                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            ////////////////////////////

            if (checkUserOrder != null)
            {

                var productordersuserorders = _context.ProductOrdersUserOrders.Where(x => x.UserOrderId == checkUserOrder.Id).ToList();
                List<ProductOrder> productOrders = new List<ProductOrder>();

                foreach (var item in productordersuserorders)
                {
                   
                     var test = _context.PorductOrders.First(x => x.Id == item.ProductOrderId);
                    productOrders.Add(test);
                }

                foreach (var item in order.Orders)
                {
                    foreach (var item2 in productOrders)
                    {
                        if (item2.ProductId == item.ProductId)
                        {
                           

                            item2.Count += item.Count;
                            _context.Update(item2);
                            _context.SaveChanges();
                        }
                        else
                        {
                            ///////// this
                            _context.PorductOrders.Add(new ProductOrder { ProductId = item2.ProductId, Count = item2.Count });
                            _context.SaveChanges();
                        }

                    }
                }

                //foreach (var item in productOrders)
                //{
                //    orderProducts = _context.Products.Where(x => x.Id == item.ProductId ).ToList();

                //}
                //if (orderProducts != null)
                //{
                //    foreach (var item in order.Orders)
                //    {
                //        foreach (var item2 in orderProducts.ToList())
                //        {
                //            if (item2.ProductId == item.ProductId)
                //            {
                //                item2.Count += item.Count;
                //                _context.Update(item2);
                //                await _context.SaveChangesAsync();
                //            }
                //            else
                //            {
                //                orderProducts.Add(new ProductOrder { ProductId = item.ProductId, Count = item.Count });

                //            }
                //        }

                //    }
                //}


                //if (orderProducts.Count > 0)
                //{
                //    _context.PorductOrders.UpdateRange(orderProducts);
                //    await _context.SaveChangesAsync();

                //    foreach (var item in orderProducts)
                //    {
                //        _context.ProductOrdersUserOrders.AddRange(new ProductOrdersUserOrders { UserOrderId = checkUserOrder.Id, ProductOrderId = item.Id });
                //        await _context.SaveChangesAsync();

                //    }
                //    return Ok();
                //}
            }


            return BadRequest();




        }

        


    }
}
