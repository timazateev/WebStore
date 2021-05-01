using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;
using WebStoreDomain.Entities.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
            _logger = Logger;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

           

            var user = new User
            {
                UserName = Model.UserName
            };

            _logger.LogInformation("User registration {0}", user.UserName);

            var registration_result = await _userManager.CreateAsync(user, Model.Password);
            if (registration_result.Succeeded)
            {
                _logger.LogInformation("User registered {0}", user.UserName);
                await _userManager.AddToRoleAsync(user, Role.Users);
                _logger.LogInformation("User {0} added to role {1}  ", user.UserName, Role.Users);
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation("User {0} has login", user.UserName);
                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning("Error during user {0} registration: {1}", 
                user.UserName, 
                string.Join(",", registration_result.Errors.Select(e => e.Description))
                );

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(Model);
        }
        #endregion
        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {

            if (!ModelState.IsValid) return View(Model);

            var login_result = await _signInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif

                );
            if (login_result.Succeeded)
            {
                _logger.LogInformation("User {0} has login", Model.UserName);
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
                return LocalRedirect(Model.ReturnUrl ?? "/");
            }
            _logger.LogWarning("Error during user login {0} or password authentication", Model.UserName);
            ModelState.AddModelError("", "Incorrect user name or password!");
            return View(Model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity.Name;
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User {0} has logout", user_name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}
