using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Chat
    {
        [Key]
        public int ID { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> Users { get; set; }
    }
}
