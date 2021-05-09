using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.ViewModels;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.DTO;
using WebStore.Services.Mapping;
using WebStore.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SqlOrderService> _logger;

        public SqlOrderService(WebStoreContext db, UserManager<User> UserManager, ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _userManager = UserManager;
            _logger = Logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
        .Include(order => order.User)
        .Include(Order => Order.Items)
        .Where(order => order.User.UserName == UserName)
        .ToArrayAsync())
        .Select(order => order.ToDTO());


        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
        .Include(order => order.User)
        .Include(Order => Order.Items)
        .FirstOrDefaultAsync(order => order.id == id))
        .ToDTO();

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
                throw new InvalidOperationException($"User with name {UserName} not found in DB");

            _logger.LogInformation("Adding new order for {0}",
                UserName);

            var timer = Stopwatch.StartNew();

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user
            };

            //var product_ids = Cart.Items.Select(item => item.Product.id).ToArray();

            //var cart_products = await _db.Products
            //    .Where(p => product_ids.Contains(p.id))
            //    .ToArrayAsync();

            //order.Items = Cart.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.id,
            //    product => product.id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price, //Discount could be added
            //        Quantity = cart_item.Quantity,
            //    }).ToArray();

            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.id);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();
            _logger.LogInformation("The order for {0} added at {1} with id:{2} with cost {3}",
                UserName, timer.Elapsed, order.id, order.Items.Sum(i => i.TotalItemPrice));

            return order.ToDTO();
        }

    }
}
