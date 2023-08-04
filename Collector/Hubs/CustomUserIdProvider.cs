using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.SignalR;

namespace Collector.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        UserManager userManager = new UserManager(new EfUserRepository());
        public string? GetUserId(HubConnectionContext connection)
        {
            return userManager.Get(Usr => Usr.Username == connection.User.Identity.Name).ID.ToString();
        }
    }
}
