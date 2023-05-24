using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? HomeAdress { get; set; }
        public string? About { get; set; }
        public string? ProfileImage { get; set; }

        public List<Product> Products { get; set; }
        public Cart? Cart { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Product> FavProducts { get; set; } = new();

        public List<FollowerList> Followers { get; set; }
        public List<FollowerList> Followings { get; set; }
    }
}
