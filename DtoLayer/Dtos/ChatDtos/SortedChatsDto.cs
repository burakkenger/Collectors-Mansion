using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.ChatDtos
{
    public class SortedChatsDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string Message { get; set; }
    }
}
