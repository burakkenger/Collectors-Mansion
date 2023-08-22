using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DtoLayer.Dtos.ChatDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace Collector.Controllers
{
    public class MessageController : Controller
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        ChatManager chatManager = new ChatManager(new EfChatRepository());
        MessageManager messageManager = new MessageManager(new EfMessageRepository());
        
        private User UserData()
        {
            return userManager.GetUserData(User.Identity.Name);
        }

        [HttpGet]
        public IActionResult Index(int ID)
        {
            if (ID == 0)
            {
                var lastMessage = messageManager.GetAll(Msg => Msg.ReceiverID == UserData().ID || Msg.SenderID == UserData().ID)
                    .OrderByDescending(Msg => Msg.Date).FirstOrDefault();

                if (lastMessage != null)
                {
                    if (lastMessage.SenderID == UserData().ID)
                        ID = lastMessage.ReceiverID;
                    else
                        ID = lastMessage.SenderID;
                }
                else
                    return RedirectToAction("ErrorPage", "Message");
            }

            var check = chatManager.GetUserChats(UserData().ID).FirstOrDefault(Cht => Cht.Users.Exists(Usr => Usr.ID == ID));

            if (check == null)
            {
                var receiverUser = userManager.Get(Usr => Usr.ID == ID);

                if (receiverUser != null)
                {
                    var chatUsers = new List<User> { receiverUser, UserData() };
                    Chat chat = new() { Users = chatUsers };
                    chatManager.Update(chat);
                }
            }

            var conversations = chatManager.GetUserChats(UserData().ID).FirstOrDefault(Cht => Cht.Users.Exists(Usr => Usr.ID == ID));

            var sortedChatList = chatManager.GetSortedUserChats(UserData().ID);
            ViewBag.Chats = sortedChatList;
            ViewBag.AuthenticatedUser = UserData();
            ViewBag.ReceiverID = ID;

            if (conversations != null)
                return View(conversations);
            else
                return RedirectToAction("ErrorPage", "Message");
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        public JsonResult SendMessage(Message message)
        {
            message.SenderID = UserData().ID;
            message.Date = DateTime.UtcNow.AddHours(3);
            int ChatID = chatManager.GetUserChats(UserData().ID).Where(Cht => Cht.Users.Exists(Usr => Usr.ID == message.ReceiverID)).FirstOrDefault().ID;
            message.ChatID = ChatID;
            messageManager.Insert(message);

            var msgDate = DateTime.UtcNow.AddHours(3);
            CultureInfo trCulture = new CultureInfo("tr-TR");
            
            var jsonAjax = new AjaxDto
            {
                SortedChat = chatManager.GetSortedUserChats(UserData().ID),
                Message = message.Content,
                Date = msgDate.ToString("dddd, dd MMMM yyyy HH:mm", trCulture)
            };

            string json = JsonConvert.SerializeObject(jsonAjax, Formatting.Indented);

            return Json(json);
        }
    }
}
