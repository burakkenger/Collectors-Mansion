using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Collector.Controllers
{
    public class ProfileController : Controller
    {
        private User UserData()
        {
            return userManager.GetUserData(User.Identity.Name);
        }

        UserManager userManager = new UserManager(new EfUserRepository());

        public IActionResult UserProfile()
        {
            var user = userManager.GetAllIncludeOthers().Where(l => l.Username == UserData().Username).FirstOrDefault();

            return View(user);
        }

        public IActionResult AddProduct()
        {

            return View();
        }
    }
}
