using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int StatusID { get; set; }
        public Status Status { get; set; }

        public int? CollectionID { get; set; }
        public Collection? Collection { get; set; }


        public List<Cart> Carts { get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }

        public List<User> ProductLikes { get; set; } = new();


    }
}
