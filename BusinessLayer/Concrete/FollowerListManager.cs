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
    public class FollowerListManager : GenericManager<FollowerList>, IFollowerListService
    {
        public FollowerListManager(IFollowerListDal EfRepository) : base(EfRepository) { }
    }
}
