using DtoLayer.Dtos.ChatDtos;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IChatDal : IGenericDal<Chat>
    {
        List<Chat> efRep_GetUserChats(int ID);
        List<SortedChatsDto> efRep_GetSortedUserChats(int ID);
    }
}
