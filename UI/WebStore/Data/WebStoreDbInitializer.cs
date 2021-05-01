using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<WebStoreDbInitializer> _Logger;

        public WebStoreDbInitializer(
            WebStoreContext db,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _userManager = UserManager;
            _roleManager = RoleManager;
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


            try
            {
                InitializeIdentityAsync().GetAwaiter().GetResult();
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

        private async Task InitializeIdentityAsync()
        {
            _Logger.LogInformation("DB Initializing Identity system");
            async Task CheckRole(string RoleName)
            {
                if (!await _roleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Role {0} is not exists. Creating...", RoleName);
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
                    _Logger.LogInformation("Role {0} Created", RoleName);
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _userManager.FindByEmailAsync(User.Administrator) is null)
            {
                _Logger.LogInformation("User administratoir does not exist. Creating...");
                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("User administratoir created");
                    await _userManager.AddToRoleAsync(admin, Role.Administrators);
                    _Logger.LogInformation("User administratoir granted with the role");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    _Logger.LogError("User administratoir created with error {0}", String.Join(",", errors));
                    throw new InvalidOperationException($"Error during administrator user creation: {String.Join(",", errors)}");

                }
            }

            _Logger.LogInformation("DB Identity system Initialized");
        }
    }
}
