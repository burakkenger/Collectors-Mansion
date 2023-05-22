using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageManager : GenericManager<Image>, IImageService
    {
        public ImageManager(IImageDal EfRepository) : base(EfRepository) { }
    }
}
