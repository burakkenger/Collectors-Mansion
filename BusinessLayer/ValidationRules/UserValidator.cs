using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class UserValidator : AbstractValidator<UserEditDto>
    {
        public UserValidator()
        {

            RuleFor(field => field.Name).NotEmpty().WithMessage("İsim kısmı boş bırakılamaz");
            RuleFor(field => field.Name).MinimumLength(2).WithMessage("İsim en az 2 karakter içermeli");
            RuleFor(field => field.Name).MaximumLength(30).WithMessage("İsim en fazla 30 karakter içerebilir");

            RuleFor(field => field.Surname).NotEmpty().WithMessage("Soyisim kısmı boş bırakılamaz");
            RuleFor(field => field.Surname).MinimumLength(2).WithMessage("Soyisim en az 2 karakter içermeli");
            RuleFor(field => field.Surname).MaximumLength(30).WithMessage("Soyisim en fazla 30 karakter içerebilir");

            RuleFor(field => field.About).NotEmpty().WithMessage("Hakkında kısmı boş bırakılamaz");
            RuleFor(field => field.About).MaximumLength(300).WithMessage("Hakkında kısmı en fazla 300 karakter içerebilir");

            RuleFor(field => field.HomeAdress).NotEmpty().WithMessage("Adres kısmı boş bırakılamaz");
            RuleFor(field => field.HomeAdress).MaximumLength(180).WithMessage("Adres kısmı en fazla 180 karakter içerebilir");
        }
    }
    
}
