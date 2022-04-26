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
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClientEntity GetClientById(int clientId)
        {
            ClientEntity result = new ClientEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,
                                                                     [NAME],
                                                                     PHONENUMBER,
                                                                     EMAIL,
                                                                     CPF,
                                                                     [ADDRESS],
                                                                     STREETNUMBER,
                                                                     CEP,
                                                                     ACTIVE,
                                                                     CREATIONDATE
                                                            FROM [CLIENT]
                                                            WHERE ID = @CLIENTID", con))
                {
                    command.Parameters.Add("@CLIENTID", SqlDbType.Int).Value = clientId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if(dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Name = dt.Rows[0]["NAME"].ToString();
                result.PhoneNumber = dt.Rows[0]["PHONENUMBER"].ToString();
                result.Email = dt.Rows[0]["EMAIL"].ToString();
                result.CPF = dt.Rows[0]["CPF"].ToString();
                result.Address = dt.Rows[0]["ADDRESS"].ToString();
                result.StreetNumber = dt.Rows[0]["STREETNUMBER"].ToString();
                result.CEP = dt.Rows[0]["CEP"].ToString();
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());

            }

            return result;
        }

        public List<ClientEntity> GetClienAll(int clientId, string cpf, string name)
        {
            List<ClientEntity> result = new List<ClientEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   ID,
                                                                      [NAME],
                                                                      PHONENUMBER,
                                                                      EMAIL,
                                                                      CPF,
                                                                      [ADDRESS],
                                                                      STREETNUMBER,
                                                                      CEP,
                                                                      ACTIVE,
                                                                      CREATIONDATE
                                                             FROM [CLIENT]
                                                             WHERE (@CLIENTID IS NULL OR ID = @CLIENTID) 
                                                             AND (@CPF IS NULL OR @CPF = '' OR CPF = @CPF)
                                                             AND (@NAME IS NULL OR @NAME = '' OR [NAME] LIKE '%'+@NAME+'%')", con))
                {
                    command.Parameters.Add("@CLIENTID", SqlDbType.Int).Value = clientId == 0 ? DBNull.Value : clientId;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = cpf;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClientEntity entity = new ClientEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();
                    entity.PhoneNumber = dt.Rows[i]["PHONENUMBER"].ToString();
                    entity.Email = dt.Rows[i]["EMAIL"].ToString();
                    entity.CPF = dt.Rows[i]["CPF"].ToString();
                    entity.Address = dt.Rows[i]["ADDRESS"].ToString();
                    entity.StreetNumber = dt.Rows[i]["STREETNUMBER"].ToString();
                    entity.CEP = dt.Rows[i]["CEP"].ToString();
                    entity.Active = bool.Parse(dt.Rows[i]["ACTIVE"].ToString());
                    entity.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());

                    result.Add(entity);
                }                

            }

            return result;
        }

        public void CreateClient(ClientEntity clientEntity)
        {           
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO CLIENT VALUES( @NAME,
                                                                                        @EMAIL,
                                                                                        @PHONENUMBER, 
                                                                                        @CPF, 
                                                                                        @ADDRESS, 
                                                                                        @STREETNUMBER, 
                                                                                        @CEP, 
                                                                                        @ACTIVE, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = clientEntity.Name;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = clientEntity.PhoneNumber;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = clientEntity.Email;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = clientEntity.CPF;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = clientEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = clientEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = clientEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = clientEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }            
        }

        public void UpdateClient(ClientEntity clientEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE CLIENT SET [NAME] = @NAME,
                                                                                       EMAIL = @EMAIL,
                                                                                       PHONENUMBER = @PHONENUMBER,
                                                                                       CPF = @CPF,
                                                                                       [ADDRESS] = @ADDRESS,
                                                                                       STREETNUMBER = @STREETNUMBER,
                                                                                       CEP = @CEP,
                                                                                       ACTIVE = @ACTIVE
                                                                    WHERE ID = @CLIENTID", con))
                {

                    command.Parameters.Add("@CLIENTID", SqlDbType.Int).Value = clientEntity.Id;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = clientEntity.Name;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = clientEntity.PhoneNumber;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = clientEntity.Email;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = clientEntity.CPF;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = clientEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = clientEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = clientEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = clientEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ClientEntity> GetClienAllResume()
        {
            List<ClientEntity> result = new List<ClientEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   ID,
                                                                      [NAME]                                                                  
                                                             FROM [CLIENT]
                                                             WHERE ACTIVE = 1", con))
                {
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClientEntity entity = new ClientEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();

                    result.Add(entity);
                }

            }

            return result;
        }

    }
}
