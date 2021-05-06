using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Services.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices CartServices) => _cartServices = CartServices;
        public IActionResult Index() => View(new CartOrderViewModel { Cart = _cartServices.GetViewModel() } );

        public IActionResult Add(int id)
        {
            _cartServices.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _cartServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _cartServices.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartServices.Clear();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartServices.GetViewModel(),
                    Order = OrderModel
                });

            //var order = await OrderService.CreateOrder(
            //    User.Identity!.Name,
            //    _CartServices.GetViewModel(),
            //    OrderModel
            //    );

            var order_model = new CreateOrderModel
            {
                Order = OrderModel,
                Items = _cartServices.GetViewModel().Items.Select(item => new OrderItemDTO
                {
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                }).ToList()
            };

            var order = await OrderService.CreateOrder(User.Identity!.Name, order_model);

            _cartServices.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
