using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IUserBusiness
    {
        void CreateUser(CreateUserInputModel createUserInputModel);
        LoginViewModel Login(LoginInputModel loginInputModel);
        void ResetPassword(int userId, string password);
        List<UserEntity> GetAllUserByFilter(string name, string email);
        UserEntity GetUser(int id);
    }
}
