using BackEnd.Applicaton.Services.Contracts;
using BackEnd.Domain.Services.Contracts;
using BackEnd.Infrastructure.Models.DTO;
using BackEnd.Infrastructure.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Applicaton.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUserDomainService _domainUserService;

        public UserService(IUserDomainService domainUserService)
        {
            _domainUserService = domainUserService;
        }

        public async Task<LoginModelDTO> getLoginModel(string username, string password)
        {
            var loginModel = await _domainUserService.getLoginModel(username, password);

            var loginModelDTO = new LoginModelDTO();
            loginModelDTO.Id = loginModel.Id;            
            loginModelDTO.UserName = loginModel.UserName;
            loginModelDTO.Password = loginModel.Password;
            loginModelDTO.RefreshToken = loginModel.RefreshToken;
            loginModelDTO.RefreshTokenExpiryTime = loginModel.RefreshTokenExpiryTime;

            return loginModelDTO;

        }

        public async Task<bool> updateLoginModel(LoginModelDTO loginModelDTO)
        {
            var loginModel = new LoginModel();
            loginModel.Id = loginModelDTO.Id;
            loginModel.UserName = loginModelDTO.UserName;
            loginModel.Password = loginModelDTO.Password;
            loginModel.RefreshToken = loginModelDTO.RefreshToken;
            loginModel.RefreshTokenExpiryTime = loginModelDTO.RefreshTokenExpiryTime;

            return await _domainUserService.updateLoginModel(loginModel);

        }
    }
}
