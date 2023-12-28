using BackEnd.Infrastructure.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Domain.Services.Contracts
{
    public interface IUserDomainService
    {
        Task<LoginModel> getLoginModel(string username, string password);

        Task<bool> updateLoginModel(LoginModel loginModel);
    }
}
