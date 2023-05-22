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
    public class CartListManager : GenericManager<CartList>, ICartListService
    {
        public CartListManager(ICartListDal EfRepository) : base(EfRepository) {}
    }
}
