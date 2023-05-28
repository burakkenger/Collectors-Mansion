using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dtos.UserDtos
{
    [ValidateNever]
    public class UserEditDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HomeAdress { get; set; }
        public string About { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
