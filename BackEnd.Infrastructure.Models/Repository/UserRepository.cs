using BackEnd.Infrastructure.Models.Model;
using BackEnd.Infrastructure.Models.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure.Models.Repository
{
    public  class UserRepository: IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<LoginModel> getLoginModel(string userName, string password)
        {
            var user = await _userContext.LoginModels?.Where(u => u.UserName == userName && u.Password == password).FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<bool> updateLoginModel(LoginModel loginModel)
        {
           var loginModelEntity = await _userContext.LoginModels.Where(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password).FirstOrDefaultAsync();

            if (loginModelEntity is null)
                return false;

            UpdateFieldsLoginModel(ref loginModelEntity, loginModel);

            var response = await _userContext.SaveChangesAsync();

            return response > 0;
          
        }

        #region Private Methods

        private void UpdateFieldsLoginModel(ref LoginModel loginModelEntity, LoginModel loginModel)
        {
            loginModelEntity.UserName = loginModel.UserName;
            loginModelEntity.Password = loginModel.Password;
            loginModelEntity.RefreshToken = loginModel.RefreshToken;
            loginModelEntity.RefreshTokenExpiryTime = loginModel.RefreshTokenExpiryTime;

        }
        #endregion
    }
}
