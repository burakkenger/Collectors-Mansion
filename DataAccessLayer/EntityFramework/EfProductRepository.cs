using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfProductRepository : GenericRepository<Product>, IProductDal
    {
        Context context = new Context();
        public List<Product> efRep_GetIncludeOthers()
        {
            return context.Products
                .Include(l => l.Images)
                .Include(l => l.ProductLikes)
                .Include(l => l.Status)
                .Include(l => l.Category)
                .Include(l => l.User)
                .Include(l => l.Comments).ThenInclude(l => l.User)
                .ToList();
        }
    }
}
