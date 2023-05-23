using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Collector.Controllers
{
    public class HomeController : Controller
    {
        private User UserData()
        {
            UserManager userManager = new UserManager(new EfUserRepository());
            return userManager.GetUserData(User.Identity.Name);
        }

        public IActionResult HomePage()
        {

            return View(UserData());
        }
    }
}
