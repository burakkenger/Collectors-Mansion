using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.ChatDtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Globalization;

namespace Collector.Hubs
{
    public class ChatHub : Hub
    {
        ChatManager chatManager = new ChatManager(new EfChatRepository());
        UserManager userManager = new UserManager(new EfUserRepository());
        
        private string SenderID()
        {
            return userManager.Get(Usr => Usr.Username == Context.User.Identity.Name).ID.ToString();
        }

        public async Task SendMessage(string RecID, string message)
        {
            CultureInfo trCulture = new CultureInfo("tr-TR");
            var SignalRDto = new SignalRMsgDto() { Message = message, Date = DateTime.UtcNow.AddHours(3).ToString("dddd, dd MMMM yyyy HH:mm", trCulture) };
            string jsonSortedChat = JsonConvert.SerializeObject(chatManager.GetSortedUserChats(Convert.ToInt32(RecID)), Formatting.Indented);
            string jsonSignalRDto = JsonConvert.SerializeObject(SignalRDto, Formatting.Indented);

            await Clients.User(RecID).SendAsync("ReceiveMessage", SenderID(), jsonSignalRDto, jsonSortedChat);
        }
    }
}
