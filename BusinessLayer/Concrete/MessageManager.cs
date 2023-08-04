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
    public class MessageManager : GenericManager<Message>, IMessageService
    {
        public MessageManager(IMessageDal EfRepository) : base(EfRepository){}
    }
}
