using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Services.Interfaces;
using WebStoreDomain;
using WebStoreDomain.Entities;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData //Unit of work
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext db) { _db = db; }

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(s => s.Products);

        public Product GetProductById(int id) => _db.Products
            .Include(p => p.Section)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.id == id);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;
            //.Include(p => p.Sections);
            //.Include(p => p.Brands);

            if (Filter?.Ids?.Length > 0)
            {
                query = query.Where(product => Filter.Ids.Contains(product.id));
            }
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);

            }

            return query;
        }

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products);
    }
}
