using BackEnd.Infrastructure.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure.Models.RepositoryContracts
{
    public interface IUserRepository
    {
        Task<LoginModel> getLoginModel(string username, string password);

        Task<bool> updateLoginModel(LoginModel loginModel);


    }
}
