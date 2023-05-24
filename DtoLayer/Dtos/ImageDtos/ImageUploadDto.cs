using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.ImageDtos
{
    public class ImageUploadDto
    {
        public List<IFormFile> ImageFiles { get; set; }
    }
}
