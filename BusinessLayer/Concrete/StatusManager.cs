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
    public class StatusManager : GenericManager<Status>, IStatusService
    {
        public StatusManager(IStatusDal EfRepository) : base(EfRepository) { }
    }
}
