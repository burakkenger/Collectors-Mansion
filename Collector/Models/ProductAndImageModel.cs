using DtoLayer.Dtos.ImageDtos;
using EntityLayer.Concrete;

namespace Collector.Models
{
    public class ProductAndImageModel
    {
        public Product Product { get; set; }
        public ImageUploadDto ImageUpload { get; set; }

    }
}
