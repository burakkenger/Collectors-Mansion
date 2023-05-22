using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
