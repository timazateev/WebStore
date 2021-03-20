using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Filters;
using WebStore.Models;

namespace WebStore.Controllers
{   
    [ActionDescription("Main Controller")]
    public class HomeController : Controller
    {
        [ActionDescription("Main Action")]
        [AddHeader("Text","Value")]
        public IActionResult Index() => View();
        public IActionResult NotFound() => View();
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult Checkout() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

    }
}
