using DentalShop.Areas.Admin.Model;
using DentalShop.Models;
using DentalShop.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<OrderProductsUsers>>> Buy(BuyDTO order)
        {
            var username = User.Identity.Name.ToString();
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);

            var response = new BuyDTO
            {
                Name = user.Id,
                Orders = order.Orders
            };

            OrderProduct orderProduct = new OrderProduct();
            orderProduct.UserId = user.Id;

            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();

            ///////////////////////////////////////

            var orders = order.Orders.Select(x => new OrderProductsUsers
            {
                UserId = user.Id,
                ProductId = x.ProductId,
                Count = x.Count
            });
            _context.OrderProductUsers.AddRange(orders);
            _context.SaveChanges();

            return orders.ToList();
        }

        [HttpPost("ping")]
        public async Task<ActionResult<string>> Ping()
        {
            return "ping";
        }


    }
}
