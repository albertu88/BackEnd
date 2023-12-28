using BackEnd.Infrastructure.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Applicaton.Services.Contracts
{
    public interface IUserService
    {
        Task<LoginModelDTO> getLoginModel(string username, string password);

        Task<bool> updateLoginModel(LoginModelDTO loginModelDTO);
    }
}
