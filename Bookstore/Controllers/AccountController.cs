using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;

namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Models.Account> _signInManager;
        private readonly UserManager<Models.Account> _userManager;

        public AccountController(SignInManager<Models.Account> signInManager, UserManager<Models.Account> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home"); // Zmienione na kontroler MVC
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Dto.Login model)
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.FindByNameAsync(model.UserName.ToLower());
                if (exists == null)
                {
                    ModelState.AddModelError(string.Empty, "User with this user name does not exist");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName.ToLower(), model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Zmienione na kontroler MVC
                }

                ModelState.AddModelError(string.Empty, "There was a problem signing you in, try later");
            }

            // Jeśli doszliśmy tutaj, coś poszło nie tak, wyświetlamy formularz ponownie
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Dto.Register model)
        {
            if (ModelState.IsValid)
            {
                var exists = await _userManager.FindByNameAsync(model.UserName.ToLower());
                if (exists != null)
                {
                    ModelState.AddModelError(string.Empty, "User name already taken try another one");
                    return View(model);
                }

                exists = await _userManager.FindByEmailAsync(model.EmailAddress.ToLower());
                if (exists != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already taken try another one");
                    return View(model);
                }

                var account = new Models.Account
                {
                    UserName = model.UserName.ToLower(),
                    Email = model.EmailAddress
                };

                var result = await _userManager.CreateAsync(account, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(account, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Przekierowanie do strony logowania po wylogowaniu
            return RedirectToAction("Login");
        }
    }
}
