using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure.Models.DTO
{
    public class LoginModel
    {
        public LoginModel()
        {
        }
        public string? UserName { get; set; }
        public string? Password { get; set; }

    }
}
