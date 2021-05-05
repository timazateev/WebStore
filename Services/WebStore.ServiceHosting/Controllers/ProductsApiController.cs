using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Product)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;
        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _ProductData.GetSections();

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _ProductData.GetBrands();

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => _ProductData.GetProducts(Filter);

        [HttpGet("{id:int}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);


    }
}
