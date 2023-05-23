using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Collector.ViewComponents.NavUserProfile
{
    public class NavUserProfile : ViewComponent
    {
        private User UserData()
        {
            UserManager userManager = new UserManager(new EfUserRepository());
            return userManager.GetUserData(User.Identity.Name);
        }

        public IViewComponentResult Invoke()
        {
            return View(UserData());
        }
    }
}
