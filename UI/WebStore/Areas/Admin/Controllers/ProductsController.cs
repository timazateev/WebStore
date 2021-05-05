using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private IProductData _ProductData;

        public ProductsController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts().FromDTO());

        public IActionResult Edit(int id) =>
            _ProductData.GetProductById(id) is { } product
            ? View(product.FromDTO())
            : NotFound();

        public IActionResult Delete(int id) =>
            _ProductData.GetProductById(id) is { } product
            ? View(product.FromDTO())
            : NotFound();
    }
}
