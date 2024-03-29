﻿using BusinessLayer.Concrete;
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
        FollowerListManager followerListManager = new FollowerListManager(new EfFollowerListRepository());
        CartManager cartManager = new CartManager(new EfCartRepository());
        CartListManager cartListManager = new CartListManager(new EfCartListRepository());

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
            product.BanStatus = false;
            


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

        public IActionResult OtherUserProfile(int ID)
        {
            if(ID == UserData().ID)
            {
                return RedirectToAction("UserProfile", "Profile");
            }
            var user = userManager.GetAllIncludeOthers().Where(l => l.ID == ID).FirstOrDefault();
            var checkFollow = followerListManager.Get(l => l.FollowedID == ID && l.FollowerID == UserData().ID);
            ViewBag.MainUser = UserData();

            if(checkFollow == null)
            {
                ViewBag.FollowStatus = 0;
            }
            else
            {
                ViewBag.FollowStatus = 1;
            }

            return View(user);
        }

        public JsonResult Follow(FollowerList followerList)
        {
            var check = followerListManager.Get(l => l.FollowedID == followerList.FollowedID && l.FollowerID == followerList.FollowerID);
            string returnCode;

            if(check == null)
            {
                followerListManager.Insert(followerList);
                returnCode = "0";
            }
            else
            {
                followerListManager.Delete(check);
                returnCode = "1";
            }

            var followerCount = followerListManager.GetAll(l => l.FollowedID == followerList.FollowedID).Count().ToString();
            List<string> returnList = new List<string>();
            returnList.Add(followerCount);
            returnList.Add(returnCode);

            return Json(returnList);
        }

        public IActionResult Favorites()
        {
            var products = productManager.GetIncludeOthers();
            var favProducts = products.Where(l => l.ProductLikes.Exists(l => l.ID == UserData().ID)).ToList();

            return View(favProducts);
        }

        public IActionResult Cart()
        {
            var values = cartManager.GetAllIncludeOthers().Where(l => l.UserID == UserData().ID).FirstOrDefault();
            return View(values);
        }

        public IActionResult DeleteProductFromCart(int ID)
        {
            var cart = cartManager.Get(l => l.UserID == UserData().ID);
            var cartProduct = cartListManager.Get(l => l.ProductID == ID && l.CartID == cart.ID);
            cartListManager.Delete(cartProduct);
            return RedirectToAction("Cart", "Profile");
        }

        public JsonResult AddProductToCart(CartViewModel cartViewModel)
        {
            string returnString;
            var currentUser = userManager.GetAllIncludeOthers().Where(l => l.ID == UserData().ID).FirstOrDefault();
            var userProductCheck = currentUser.Products.Where(l => l.ID == cartViewModel.ProductID).FirstOrDefault();

            if (userProductCheck != null) 
            {
                returnString = "Kendi ürününüzü sepete ekleyemezsiniz";
                return Json(returnString);
            }

            
            var cart = cartManager.Get(l => l.UserID == UserData().ID);
            var check = cartListManager.Get(l => l.ProductID == cartViewModel.ProductID && l.CartID == cart.ID);
            CartList cartList = new CartList();
            cartList.CartID = cart.ID;
            cartList.ProductID = cartViewModel.ProductID;

            if (check != null)
            {
                returnString = "Ürün zaten sepetinizde";
            }
            else 
            {
                cartListManager.Insert(cartList);
                returnString = "Ürün sepete eklendi!";
            }
                
            return Json(returnString);
        }



    }
}

