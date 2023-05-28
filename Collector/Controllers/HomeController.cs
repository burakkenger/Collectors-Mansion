using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.ProductDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Collector.Controllers
{
    public class HomeController : Controller
    {
        private User UserData()
        {
            UserManager userManager = new UserManager(new EfUserRepository());
            return userManager.GetUserData(User.Identity.Name);
        }

        ProductManager productManager = new ProductManager(new EfProductRepository());
        CommentManager commentManager = new CommentManager(new EfCommentRepository());

        public IActionResult HomePage(string keyword)
        {
            if(!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                var filteredProducts = productManager.GetIncludeOthers();
                filteredProducts = filteredProducts.Where(x => x.Name.ToLower().Contains(keyword)).ToList();
                ViewBag.User = UserData();
                return View(filteredProducts);
            }
            var products = productManager.GetIncludeOthers();
            ViewBag.User = UserData();
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductPage(int ID) 
        {
            var productCommentDto = new ProductCommentDto();
            var product = productManager.GetIncludeOthers().Where(l => l.ID == ID).FirstOrDefault();
            ViewBag.Product = product;

            return View(productCommentDto);
        }

        [HttpPost]
        public IActionResult ProductPage(ProductCommentDto productComment)
        {
            var product = productManager.GetIncludeOthers().Where(l => l.ID == productComment.ProductID && l.BanStatus == false).FirstOrDefault();
            ViewBag.Product = product;

            var comment = new Comment();
            comment.Content = productComment.Comment;
            comment.ProductID = productComment.ProductID;
            comment.UserID = UserData().ID;
            comment.Date = DateTime.Now;


            CommentValidator validator = new CommentValidator();
            ValidationResult result = validator.Validate(productComment);

            if (result.IsValid)
            {
                commentManager.Insert(comment);
                return RedirectToAction("ProductPage", "Home", new { ID = productComment.ProductID });
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

        public JsonResult postLike(int ProductID, string value)
        {
            //newsletterManager.Insert(data);
            var jsonResult = JsonConvert.SerializeObject(value);

            return Json(jsonResult);
        }



    }
}
