using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.ViewModels;
using WebStoreDomain.Entities.Identity;
using WebStoreDomain.Entities.Orders;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext db, UserManager<User> UserManager) 
        {
            _db = db;
            _userManager = UserManager;
        }

        public async Task<IEnumerable<Order>> GetUserOrder(string UserName) => await _db.Orders
        .Include(order => order.User)
        .Include(Order => Order.Items)
        .Where(order => order.User.UserName == UserName)
        .ToArrayAsync();


        public async Task<Order> GetOrderById(int id) => await _db.Orders
        .Include(order => order.User)
        .Include(Order => Order.Items)
        .FirstOrDefaultAsync(order => order.id == id);

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
                throw new InvalidOperationException($"User with name {UserName} not found in DB");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order 
            {
                Name = OrderModel.Name,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                User = user
            };

            var product_ids = Cart.Items.Select(item => item.Product.id).ToArray();

            var cart_products = await _db.Products
                .Where(p => product_ids.Contains(p.id))
                .ToArrayAsync();

            order.Items = Cart.Items.Join(
                cart_products,
                cart_item => cart_item.Product.id,
                product => product.id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price, //Discount could be added
                    Quantity = cart_item.Quantity,
                }).ToArray();

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }

    }
}
