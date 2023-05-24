using BusinessLayer.Concrete;
using Collector.Models;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.ImageDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Collector.Controllers
{
    public class ProfileController : Controller
    {
        private User UserData()
        {
            return userManager.GetUserData(User.Identity.Name);
        }

        UserManager userManager = new UserManager(new EfUserRepository());
        ProductManager productManager = new ProductManager(new EfProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        StatusManager statusManager = new StatusManager(new EfStatusRepository());

        public IActionResult UserProfile()
        {
            var user = userManager.GetAllIncludeOthers().Where(l => l.Username == UserData().Username).FirstOrDefault();

            return View(user);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> Categories = (from x in categoryManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            ViewBag.CategoryList = Categories;

            List<SelectListItem> Statuses = (from x in statusManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            ViewBag.StatusList = Statuses;

            ViewBag.UserData = UserData();

            var viewModel = new ProductAndImageModel(); {};

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductAndImageModel viewModel)
        {
            var imageList = new List<Image>
            {};

            if (viewModel.ImageUpload != null)
            {
                foreach (var iFormFileImage in viewModel.ImageUpload.ImageFiles)
                {
                    var newImage = new Image();
                    var extension = Path.GetExtension(iFormFileImage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    iFormFileImage.CopyTo(stream);
                    newImage.FilePath = "/Images/ProductImages/" + newImageName;
                    imageList.Add(newImage);
                }
            }

            var product = new Product();

            product.Name = viewModel.Product.Name;
            product.Description = viewModel.Product.Description;
            product.UserID = viewModel.Product.UserID;
            product.CategoryID = viewModel.Product.CategoryID;
            product.StatusID = viewModel.Product.StatusID;
            product.Images = imageList;
            productManager.Insert(product);

            return RedirectToAction("UserProfile", "Profile"); 

        }
    }
}
