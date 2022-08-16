using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.API.Core.Interface
{
    public interface IUserRepository
    {
        void AlterUser(UserEntity userEntity, bool setFirtLogin);
        void CreateUser(UserEntity userEntity);
        UserEntity GetUserById(int id);
        UserEntity GetUserByUserName(string userName);
        void ResetPassword(int userId, string password);
        List<UserEntity> GetAllUserByFilter(string name, string email);
    }
}
