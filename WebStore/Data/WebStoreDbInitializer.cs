using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebStore.DAL.Context;
using WebStoreDomain;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreContext _db;
        private readonly ILogger<WebStoreDbInitializer> _Logger;

        public WebStoreDbInitializer(WebStoreContext db, ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public void Initialize()
        {

            _Logger.LogInformation("DB Initializing...");
            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _Logger.LogInformation("DB migration is in progress...");
                _db.Database.Migrate();
                _Logger.LogInformation("DB migration completed");
            }

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {
                _Logger.LogError("An error occured in during products initializing in Database");
            }
            _Logger.LogInformation("DB Initialized");

        }

        private void InitializeProducts()
        {



            if (_db.Products.Any())
            {
                _Logger.LogInformation("Products initializing not required");
                return;
            }
            _Logger.LogInformation("Sections initializing...");

            var products_sections = TestData.Sections.Join(
                TestData.Products,
                section => section.id,
                product => product.SectionId,
                (section, product) => (section, product));

            foreach (var (section, product) in products_sections)
            {
                section.Products.Add(product);
            }

            var products_brands = TestData.Brands.Join(
                TestData.Products,
                brand => brand.id,
                product => product.BrandId,
                (brand, product) => (brand, product));

            foreach (var (brand, product) in products_brands)
            {
                brand.Products.Add(product);
            }

            var section_section = TestData.Sections.Join(
                TestData.Sections,
                parent => parent.id,
                child => child.ParentId,
                (parent, child) => (parent, child));

            foreach (var (parent, child) in section_section)
            {
                child.Parent = parent;
            }

            foreach (var product in TestData.Products)
            {
                product.id = 0;
                product.BrandId = null;
                product.SectionId = 0;
            }

            foreach (var brand in TestData.Brands)
            {
                brand.id = 0;
            }

            foreach (var section in TestData.Sections)
            {
                section.id = 0;
                section.ParentId = null;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);
                
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            
            _Logger.LogInformation("Products initialize completed.");

        }
    }
}
