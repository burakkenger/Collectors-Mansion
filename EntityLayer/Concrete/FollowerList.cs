using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FollowerList
    {
        [Key]
        public int ID { get; set; }
        public int? FollowerID { get; set; }
        public int? FollowedID { get; set; }

        public User? Follower { get; set; }
        public User? Followed { get; set; }
    }
}
