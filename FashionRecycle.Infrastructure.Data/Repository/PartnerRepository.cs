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
    public class PartnerRepository : IPartnerRepository
    {
        private readonly IConfiguration _configuration;

        public PartnerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PartnerEntity GetPartnerById(int partnerId)
        {
            PartnerEntity result = new PartnerEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,
                                                                     [NAME],
                                                                     EMAIL,
                                                                     PHONENUMBER,
                                                                     CPF,
                                                                     CNPJ,
                                                                     [ADDRESS],
                                                                     STREETNUMBER,
                                                                     CEP,
                                                                     ACTIVE,
                                                                     CREATIONDATE
                                                            FROM [PARTNER]
                                                            WHERE ID = @PARTNERID", con))
                {
                    command.Parameters.Add("@PARTNERID", SqlDbType.Int).Value = partnerId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Name = dt.Rows[0]["NAME"].ToString();
                result.Email = dt.Rows[0]["EMAIL"].ToString();
                result.PhoneNumber = dt.Rows[0]["PHONENUMBER"].ToString();
                result.CPF = dt.Rows[0]["CPF"].ToString();
                result.CNPJ = dt.Rows[0]["CNPJ"].ToString();
                result.Address = dt.Rows[0]["ADDRESS"].ToString();
                result.StreetNumber = dt.Rows[0]["STREETNUMBER"].ToString();
                result.CEP = dt.Rows[0]["CEP"].ToString();
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());

            }

            return result;
        }

        public List<PartnerEntity> GetPartnerAll(int partnerId, string cpf, string cnpj ,string name)
        {
            List<PartnerEntity> result = new List<PartnerEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   ID,
                                                                      [NAME],
                                                                      PHONENUMBER,
                                                                      EMAIL,
                                                                      CPF,
                                                                      CNPJ,
                                                                      [ADDRESS],
                                                                      STREETNUMBER,
                                                                      CEP,
                                                                      ACTIVE,
                                                                      CREATIONDATE
                                                             FROM [PARTNER]
                                                             WHERE (@PARTNERID IS NULL OR ID = @PARTNERID) 
                                                             AND (@CPF IS NULL OR @CPF = '' OR CPF = @CPF)
                                                             AND (@CNPJ IS NULL OR @CNPJ = '' OR CPF = @CNPJ)
                                                             AND (@NAME IS NULL OR @NAME = '' OR [NAME] LIKE '%'+@NAME+'%')", con))
                {
                    command.Parameters.Add("@PARTNERID", SqlDbType.Int).Value = partnerId == 0 ? DBNull.Value : partnerId;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = cpf;
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = cnpj;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PartnerEntity entity = new PartnerEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();
                    entity.PhoneNumber = dt.Rows[i]["PHONENUMBER"].ToString();
                    entity.Email = dt.Rows[i]["EMAIL"].ToString();
                    entity.CPF = dt.Rows[i]["CPF"].ToString();
                    entity.CNPJ = dt.Rows[i]["CNPJ"].ToString();
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

        public void CreatePartner(PartnerEntity partnerEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO PARTNER VALUES( @NAME,
                                                                                        @EMAIL,
                                                                                        @PHONENUMBER,                                                                                        
                                                                                        @CPF,
                                                                                        @CNPJ,
                                                                                        @ADDRESS, 
                                                                                        @STREETNUMBER, 
                                                                                        @CEP, 
                                                                                        @ACTIVE, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = partnerEntity.Name;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = partnerEntity.Email;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = partnerEntity.PhoneNumber;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = partnerEntity.CPF;
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = partnerEntity.CNPJ;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = partnerEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = partnerEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = partnerEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = partnerEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePartner(PartnerEntity partnerEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PARTNER SET [NAME] = @NAME,
                                                                                       EMAIL = @EMAIL,
                                                                                       PHONENUMBER = @PHONENUMBER,
                                                                                       CPF = @CPF,
                                                                                       CNPJ = @CNPJ,
                                                                                       [ADDRESS] = @ADDRESS,
                                                                                       STREETNUMBER = @STREETNUMBER,
                                                                                       CEP = @CEP,
                                                                                       ACTIVE = @ACTIVE
                                                                    WHERE ID = @PARTNERID", con))
                {

                    command.Parameters.Add("@PARTNERID", SqlDbType.Int).Value = partnerEntity.Id;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = partnerEntity.Name;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = partnerEntity.Email;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = partnerEntity.PhoneNumber;
                    command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = partnerEntity.CPF;
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = partnerEntity.CNPJ;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = partnerEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = partnerEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = partnerEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = partnerEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<PartnerEntity> GetAllPartnersResumeList()
        {
            List<PartnerEntity> result = new List<PartnerEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   ID,
                                                                      [NAME]
                                                             FROM [PARTNER]
                                                             WHERE ACTIVE = 1", con))
                {                    
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PartnerEntity entity = new PartnerEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();                    

                    result.Add(entity);
                }

            }

            return result;
        }
    }
}
