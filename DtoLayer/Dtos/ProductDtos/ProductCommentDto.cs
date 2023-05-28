using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.ProductDtos
{
    [ValidateNever]
    public class ProductCommentDto
    {
        public int ProductID { get; set; }
        public string Comment { get; set; }
    }
}
