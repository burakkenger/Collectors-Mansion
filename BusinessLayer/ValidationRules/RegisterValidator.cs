using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.UserDtos;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class RegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(field => field.Username).NotEmpty().WithMessage("Kullanıcı adı kısmı boş bırakılamaz");
            RuleFor(field => field.Username).MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter içermeli");
            RuleFor(field => field.Username).MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter içerebilir");
            RuleFor(field => field.Username).Must(IsUsernameExist).WithMessage("Kullanıcı adı zaten mevcut");

            RuleFor(field => field.Email).Must(IsEmailExist).WithMessage("Bu mail adresi zaten kullanımda");

            RuleFor(field => field.Name).NotEmpty().WithMessage("İsim kısmı boş bırakılamaz");
            RuleFor(field => field.Name).MinimumLength(2).WithMessage("İsim en az 2 karakter içermeli");
            RuleFor(field => field.Name).MaximumLength(30).WithMessage("İsim en fazla 30 karakter içerebilir");

            RuleFor(field => field.Surname).NotEmpty().WithMessage("Soyisim kısmı boş bırakılamaz");
            RuleFor(field => field.Surname).MinimumLength(2).WithMessage("Soyisim en az 2 karakter içermeli");
            RuleFor(field => field.Surname).MaximumLength(30).WithMessage("Soyisim en fazla 30 karakter içerebilir");

            RuleFor(field => field.Password).NotEmpty().WithMessage("Şifre kısmı boş bırakılamaz");
            RuleFor(field => field.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter içermeli");
            RuleFor(field => field.Password).MaximumLength(30).WithMessage("Şifre en fazla 30 karakter içerebilir");
            RuleFor(field => field.Password).Equal(field => field.ConfPassword).WithMessage("Girdiğiniz şifreler eşleşmiyor");
        }

        public bool IsUsernameExist(string username)
        {
            UserManager userManager = new UserManager(new EfUserRepository());
            var value = userManager.Get(l => l.Username == username);
            
            if (value != null) 
            {
                return false;
            }

            return true;
        }

        public bool IsEmailExist(string email)
        {
            UserManager userManager = new UserManager(new EfUserRepository());
            var value = userManager.Get(l => l.Email == email);

            if (value != null)
            {
                return false;
            }

            return true;
        }
    }
}
