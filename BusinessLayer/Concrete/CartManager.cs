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
    public class CartManager : GenericManager<Cart>, ICartService 
    {
        ICartDal cartDal;
        public CartManager(ICartDal EfRepository) : base(EfRepository) 
        {
            cartDal = EfRepository;
        }

        public List<Cart> GetAllIncludeOthers()
        {
            return cartDal.efRep_GetAllIncludeOthers();
        }
    }
}
