using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebStore.DAL.Context;

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

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Brands initializing...");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Products initializing...");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
                _db.Database.CommitTransaction();
            }

            _Logger.LogInformation("Products initialize completed.");

        }
    }
}
