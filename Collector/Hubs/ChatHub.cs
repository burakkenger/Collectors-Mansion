using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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
            string json = JsonConvert.SerializeObject(chatManager.GetSortedUserChats(Convert.ToInt32(RecID)), Formatting.Indented);

            await Clients.User(RecID).SendAsync("ReceiveMessage", SenderID(), message, json);
        }
    }
}
