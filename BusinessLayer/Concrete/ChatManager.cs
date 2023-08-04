using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.Dtos.ChatDtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ChatManager : GenericManager<Chat>, IChatService
    {
        IChatDal chatDal;
        public ChatManager(IChatDal EfRepository) : base(EfRepository)
        {
            chatDal = EfRepository;
        } 

        public List<Chat> GetUserChats(int ID)
        {
            return chatDal.efRep_GetUserChats(ID);
        }

        public List<SortedChatsDto> GetSortedUserChats(int ID)
        {
            return chatDal.efRep_GetSortedUserChats(ID);
        }
    }   
}
