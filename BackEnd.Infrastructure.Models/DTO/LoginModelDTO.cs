using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure.Models.DTO
{
    public class LoginModelDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        
    }
}
