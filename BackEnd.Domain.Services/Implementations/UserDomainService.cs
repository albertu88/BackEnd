using BackEnd.Domain.Services.Contracts;
using BackEnd.Infrastructure.Models.Model;
using BackEnd.Infrastructure.Models.Repository;
using BackEnd.Infrastructure.Models.RepositoryContracts;

namespace BackEnd.Domain.Services.Implementations
{
    public class UserDomainService: IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<LoginModel> getLoginModel(string username, string password)
        {
            return _userRepository.getLoginModel(username, password);
        }

        public Task<bool> updateLoginModel(LoginModel loginModel)
        {
            return _userRepository.updateLoginModel(loginModel);
        }
    }
}