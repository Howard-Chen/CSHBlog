using CSHBlog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CSHBlog.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

            var createRes = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (createRes.Succeeded)
            {
                //Add user to user role
                var roleRes = await _userManager.AddToRoleAsync(user, "User");

                if (roleRes.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }

            //Show Error Msg
            return View("Register");
        }

        [HttpGet]
        public IActionResult LogIn(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel loginViewModel)
        {
            var signInRes = await _signinManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            if (signInRes != null && signInRes.Succeeded)
            {

                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }


                return RedirectToAction("Index", "Home");
            }
            //show error
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();

            return RedirectToAction("LogIn");
        }


        [HttpGet]
        public  IActionResult AccessDenied()
        {
            return View();
        }
    }
}
