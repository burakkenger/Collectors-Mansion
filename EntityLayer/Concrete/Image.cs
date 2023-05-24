using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public string FilePath { get; set; }

        public int? ProductID { get; set; }
        public Product? Product { get; set; }
    }
}
