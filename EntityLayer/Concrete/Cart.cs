using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public User? User { get; set; }

        public List<Product> Products { get; }
    }
}
