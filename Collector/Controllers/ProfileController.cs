using BusinessLayer.Concrete;
using BusinessLayer.ValidationModels;
using BusinessLayer.ValidationRules;
using Collector.Models;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.ImageDtos;
using DtoLayer.Dtos.UserDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Linq.Expressions;

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

            var viewModel = new ProductAndImageModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductAndImageModel viewModel)
        {
            var imageList = new List<Image>{};

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
            


            ProductValidator validator = new ProductValidator();
            ValidationResult result = validator.Validate(viewModel);

            if (result.IsValid)
            {
                productManager.Insert(product);
                return RedirectToAction("UserProfile", "Profile");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

                List<SelectListItem> Categories = (from x in categoryManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                ViewBag.CategoryList = Categories;

                List<SelectListItem> Statuses = (from x in statusManager.GetAll() select new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                ViewBag.StatusList = Statuses;

                ViewBag.UserData = UserData();
                var returnViewModel = new ProductAndImageModel();

                return View(returnViewModel);
            }
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            ViewBag.PasswordError = 0;
            var currentUser = userManager.GetByID(UserData().ID);
            var userEditDto = new UserEditDto();
            userEditDto.Name = currentUser.Name;
            userEditDto.Surname = currentUser.Surname;
            userEditDto.HomeAdress = currentUser.HomeAdress;
            userEditDto.About = currentUser.About;

            return View(userEditDto);
        }

        [HttpPost]
        public IActionResult EditProfile(UserEditDto userEdit, string verifyPassword)
        {

            if (verifyPassword == UserData().Password)
            {
                Expression<Func<User, bool>> predicate = l => l.ID == UserData().ID;

                if (userEdit.ProfileImage != null)
                {
                    var extension = Path.GetExtension(userEdit.ProfileImage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProfileImages/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    userEdit.ProfileImage.CopyTo(stream);
                    userManager.Edit(predicate, l => l.SetProperty(l => l.ProfileImage, "/Images/ProfileImages/" + newImageName));
                }

                UserValidator validator = new UserValidator();
                ValidationResult result = validator.Validate(userEdit);

                if (result.IsValid)
                {
                    userManager.Edit(predicate, l => l.SetProperty(l => l.Name, userEdit.Name));
                    userManager.Edit(predicate, l => l.SetProperty(l => l.Surname, userEdit.Surname));
                    userManager.Edit(predicate, l => l.SetProperty(l => l.HomeAdress, userEdit.HomeAdress));
                    userManager.Edit(predicate, l => l.SetProperty(l => l.About, userEdit.About));

                    return RedirectToAction("UserProfile", "Profile");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }

                    var currentUser = userManager.GetByID(UserData().ID);
                    var userEditDto = new UserEditDto();
                    userEditDto.Name = currentUser.Name;
                    userEditDto.Surname = currentUser.Surname;
                    userEditDto.HomeAdress = currentUser.HomeAdress;
                    userEditDto.About = currentUser.About;

                    return View(userEditDto);
                }
            }
            else
            {
                var currentUser = userManager.GetByID(UserData().ID);
                var userEditDto = new UserEditDto();
                userEditDto.Name = currentUser.Name;
                userEditDto.Surname = currentUser.Surname;
                userEditDto.HomeAdress = currentUser.HomeAdress;
                userEditDto.About = currentUser.About;

                ViewBag.PasswordError = 1;
                return View(userEditDto);
            }
            

        }



    }
}

