using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Mapping;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.ViewModels;
using WebStoreDomain;
using WebStoreDomain.Entities;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class InCookiesCartService : ICartServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductData _productData;
        private string _cartName;
        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;

                var cart_cookies = context.Request.Cookies[_cartName];
                if (cart_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookies(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set => ReplaceCookies(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie);
        }

        public InCookiesCartService(IHttpContextAccessor HttpContextAccessor, IProductData ProductData)
        {
            _httpContextAccessor = HttpContextAccessor;
            _productData = ProductData;

            var user = HttpContextAccessor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            _cartName = $"WebStore.Cart{user_name}";
        }

        public void Add(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            
            if(item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity >= 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);
            Cart = cart;
        }

        public void Clear()
        {
            //Cart = new Cart(); // can be so

            // other approach
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }


        public CartViewModel GetViewModel()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToArray()
            });

            var products_views = products.ToView().ToDictionary(p => p.id);

            return new CartViewModel
            {
                Items = Cart.Items
                    .Where(item => products_views.ContainsKey(item.ProductId))
                    .Select(item => (products_views[item.ProductId], item.Quantity))
            };
        }
    }
}
