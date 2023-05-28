using DtoLayer.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CommentValidator : AbstractValidator<ProductCommentDto>
    {
        public CommentValidator() 
        {
            RuleFor(field => field.Comment).MaximumLength(300).WithMessage("Yorum en fazla 300 karakter içerebilir");
            RuleFor(field => field.Comment).NotEmpty().WithMessage("Yorum kısmı boş şekilde gönderilemez");
        }   

    }
}
