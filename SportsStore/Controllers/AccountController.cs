using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private string _role = "Customer";

        public AccountController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> user,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signIn;
            _userManager = user;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl,
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                        model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if(await _roleManager.FindByNameAsync(_role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(_role));
                }

                var user = new IdentityUser
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, _role);
                    return RedirectToAction("Login");
                } 
                else
                {
                    ModelState.AddModelError("", "Unable to register new user");
                }
            }
            return View(registerModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
