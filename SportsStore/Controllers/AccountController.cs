using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signIn, UserManager<IdentityUser> user)
        {
            _signInManager = signIn;
            _userManager = user;
        }


        public ActionResult Login()
        {
            return View();
        }

    }
}
