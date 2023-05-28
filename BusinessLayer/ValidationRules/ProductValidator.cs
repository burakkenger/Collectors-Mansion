using BusinessLayer.ValidationModels;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductValidator : AbstractValidator<ProductAndImageModel>
    {
        public ProductValidator()
        {
            RuleFor(field => field.Product.Name).NotEmpty().WithMessage("Ad kısmı boş geçilemez");
            RuleFor(field => field.Product.Name).MaximumLength(35).WithMessage("Ad kısmı 35 karakteri geçemez");

            RuleFor(field => field.Product.Description).NotEmpty().WithMessage("Açıklama kısmı boş geçilemez");
        }
    }
}
