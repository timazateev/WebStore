using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Interfaces;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Domain.DTO;
using WebStore.Domain;
using System.Net.Http;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebAPI.Products) { }
        
        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");


        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            Post(Address, Filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<IEnumerable<ProductDTO>>()
                .Result;
        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");

    }
}
