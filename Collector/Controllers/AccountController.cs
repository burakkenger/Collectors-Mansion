using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.UserDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Security.Claims;

namespace Collector.Controllers
{
    public class AccountController : Controller
    {
        private User UserData()
        {
            return userManager.GetUserData(User.Identity.Name);
        }

        UserManager userManager = new UserManager(new EfUserRepository());
        CartManager cartManager = new CartManager(new EfCartRepository());

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var loginData = userManager.Login(user);

            if (loginData != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginData.Username),
                };
                var userIdentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal); // şifreli olarak cookie oluşturması için

                return RedirectToAction("HomePage", "Home");
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegister)
        {
            User user = new User();
            Cart cart = new Cart();

            user.Name = userRegister.Name;
            user.Surname = userRegister.Surname;
            user.Username = userRegister.Username;
            user.Email = userRegister.Email;
            user.Password = userRegister.Password;
            

            RegisterValidator validator = new RegisterValidator();
            ValidationResult result = validator.Validate(userRegister);

            if (result.IsValid)
            {
                user.Cart = cart;
                userManager.Insert(user);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }
    }
}
