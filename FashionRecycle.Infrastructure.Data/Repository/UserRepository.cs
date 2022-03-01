using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserEntity GetUserById(int userId)
        {
            UserEntity result = new UserEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT ID,
                                                                    USERNAME,
                                                                    [PASSWORD],
                                                                    NAME,
                                                                    EMAIL,
                                                                    [ROLEID],
                                                                    ACTIVE,
                                                                    CREATIONDATE
                                                             FROM [USER]
                                                             WHERE ID = @USERID", con))
                {
                    command.Parameters.Add("@USERID", SqlDbType.Int).Value = userId;
                    dt.Load(command.ExecuteReader());
                }

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                result.UserName = dt.Rows[i]["USERNAME"].ToString();
                result.Password = dt.Rows[i]["[PASSWORD]"].ToString();
                result.Name = dt.Rows[i]["NAME"].ToString();
                result.Email = dt.Rows[i]["EMAIL"].ToString();
                result.RoleId = int.Parse(dt.Rows[i]["[ROLEID]"].ToString());
                result.Active = dt.Rows[i]["ACTIVE"].ToString() == "1" ? true : false;
                result.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());

            }

            return result;
        }

        public UserEntity GetUserByUserName(string userName)
        {
            UserEntity result = new UserEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT ID,
                                                                    USERNAME,
                                                                    [PASSWORD],
                                                                    NAME,
                                                                    EMAIL,
                                                                    [ROLEID],
                                                                    ACTIVE,
                                                                    CREATIONDATE
                                                             FROM [USER]
                                                             WHERE USERNAME = @USERNAME", con))
                {
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = userName;
                    dt.Load(command.ExecuteReader());
                }

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                result.UserName = dt.Rows[i]["USERNAME"].ToString();
                result.Password = dt.Rows[i]["PASSWORD"].ToString();
                result.Name = dt.Rows[i]["NAME"].ToString();
                result.Email = dt.Rows[i]["EMAIL"].ToString();
                result.RoleId = int.Parse(dt.Rows[i]["ROLEID"].ToString());
                result.Active = dt.Rows[i]["ACTIVE"].ToString() == "1" ? true : false;
                result.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());

            }

            return result;
        }

        public void CreateUser(UserEntity userEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                DataTable dt = new DataTable();
                con.Open();
                using (SqlCommand command = new SqlCommand(@"
                                                INSERT INTO [USER]
                                                VALUES(@USERNAME, 
                                                @PASSWORD, 
                                                @NAME, 
                                                @EMAIL, 
                                                2, 
                                                1, 
                                                GETDATE())", con))
                {
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = userEntity.UserName;
                    command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = userEntity.Password;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = userEntity.Name;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = userEntity.Email;
                    command.ExecuteScalar();
                }

            }
        }

        public void AlterUser(UserEntity userEntity)
        {

        }
    }
}
