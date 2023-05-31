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
    public class EfCartRepository : GenericRepository<Cart>, ICartDal
    {
        Context context = new Context();
        public List<Cart> efRep_GetAllIncludeOthers()
        {
            return context.Carts
                .Include(l => l.Products).ThenInclude(l => l.Category)
                .Include(l => l.Products).ThenInclude(l => l.Images)
                .Include(l => l.User)
                .ToList();
        }
    }
}
