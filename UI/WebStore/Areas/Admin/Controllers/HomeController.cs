using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin"), Authorize(Roles = Role.Administrators)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
