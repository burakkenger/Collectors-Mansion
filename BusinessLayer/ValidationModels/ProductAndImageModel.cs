using DtoLayer.Dtos.ImageDtos;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationModels
{
    [ValidateNever]
    public class ProductAndImageModel
    {
        public Product Product { get; set; }
        public ImageUploadDto ImageUpload { get; set; }
    }
}
