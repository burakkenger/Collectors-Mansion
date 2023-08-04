using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public int ChatID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Chat Chat { get; set; }
    }
}
