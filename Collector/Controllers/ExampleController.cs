using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace Collector.Controllers
{
    public class ExampleController : Controller
    {
        Context data = new Context();
        public IActionResult Index()
        {
            var values = data.Users.Include(l => l.Cart).ThenInclude(l => l.Products).Where(l => l.ID == 1).FirstOrDefault();

             //data.Users.Where(l => l.ID == 1).ExecuteUpdate(l => l.SetProperty(l => l.Email, "denme123"));

            void Edit(Expression<Func<User, bool>> predicate, Expression<Func<SetPropertyCalls<User>, SetPropertyCalls<User>>> expression)
            {
                data.Users.Where(predicate).ExecuteUpdate(expression);
            }

            Edit(l => l.ID == 1, l => l.SetProperty(l => l.Email, "deneme"));



            return View(values);
        }
    }
}