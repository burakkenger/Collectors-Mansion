using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.UserDtos
{
    [ValidateNever]
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
