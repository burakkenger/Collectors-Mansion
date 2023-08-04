using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.ChatDtos
{
    public class AjaxDto
    {
        public List<SortedChatsDto>? SortedChat { get; set; }
        public string? Message { get; set; }
    }
}
