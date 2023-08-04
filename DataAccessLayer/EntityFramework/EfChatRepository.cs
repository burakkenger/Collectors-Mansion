using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.Dtos.ChatDtos;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfChatRepository : GenericRepository<Chat>, IChatDal
    {
        Context Db = new Context();

        public List<Chat> efRep_GetUserChats(int ID)
        {
            var userList = Db.Users
                .Include(Usr => Usr.Chats).ThenInclude(Cht => Cht.Messages).ThenInclude(Msg => Msg.Sender).ToList();
                
            return userList.FirstOrDefault(Usr => Usr.ID == ID).Chats.ToList();
        }
        public List<SortedChatsDto> efRep_GetSortedUserChats(int ID)
        {
            var sortedChats = efRep_GetUserChats(ID).OrderByDescending(Cht => Cht.Messages.Any() ? Cht.Messages.Last().Date : DateTime.MinValue);
            var ChatDto = new List<SortedChatsDto>();

            foreach (var data in sortedChats)
            {
                var user = data.Users.FirstOrDefault(Usr => Usr.ID != ID);
                var chat = new SortedChatsDto();
                chat.ID = data.ID;
                chat.Username = user.Username;
                chat.ProfileImage = user.ProfileImage;

                if (data.Messages.LastOrDefault() != null)
                    chat.Message = data.Messages.Select(message => message.Content.Length > 15 ? message.Content.Substring(0, 15) : message.Content).LastOrDefault();
                else
                    chat.Message = "";

                ChatDto.Add(chat);
            }

            return ChatDto;
        }


    }
}
