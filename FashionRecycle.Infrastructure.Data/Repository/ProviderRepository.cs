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
    public class ProviderRepository : IProviderRepository
    {
        private readonly IConfiguration _configuration;

        public ProviderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProviderEntity GetProviderById(int providerId)
        {
            ProviderEntity result = new ProviderEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,
                                                                     COMPANYNAME,
                                                                     LEGALCOMPANEYNAME,
                                                                     EMAIL,
                                                                     PHONENUMBER,                                                                     
                                                                     CNPJ,
                                                                     [ADDRESS],
                                                                     STREETNUMBER,
                                                                     CEP,
                                                                     ACTIVE,
                                                                     CREATIONDATE
                                                            FROM [PROVIDER]
                                                            WHERE ID = @PROVIDERID", con))
                {
                    command.Parameters.Add("@PROVIDERID", SqlDbType.Int).Value = providerId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.CompanyName = dt.Rows[0]["COMPANYNAME"].ToString();
                result.LegalCompanyName = dt.Rows[0]["LEGALCOMPANEYNAME"].ToString();
                result.Email = dt.Rows[0]["EMAIL"].ToString();
                result.PhoneNumber = dt.Rows[0]["PHONENUMBER"].ToString();                
                result.CNPJ = dt.Rows[0]["CNPJ"].ToString();
                result.Address = dt.Rows[0]["ADDRESS"].ToString();
                result.StreetNumber = dt.Rows[0]["STREETNUMBER"].ToString();
                result.CEP = dt.Rows[0]["CEP"].ToString();
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());

            }

            return result;
        }

        public List<ProviderEntity> GetProviderAll(int providerId, string cnpj)
        {
            List<ProviderEntity> result = new List<ProviderEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,
                                                                     COMPANYNAME,
                                                                     LEGALCOMPANEYNAME,
                                                                     EMAIL,
                                                                     PHONENUMBER,                                                                     
                                                                     CNPJ,
                                                                     [ADDRESS],
                                                                     STREETNUMBER,
                                                                     CEP,
                                                                     ACTIVE,
                                                                     CREATIONDATE
                                                            FROM [PROVIDER]
                                                             WHERE (@PROVIDERID IS NULL OR ID = @PROVIDERID)                                                              
                                                             AND (@CNPJ IS NULL OR @CNPJ = '' OR CNPJ = @CNPJ)", con))
                {
                    command.Parameters.Add("@PROVIDERID", SqlDbType.Int).Value = providerId == 0 ? DBNull.Value : providerId;                    
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = cnpj;                    
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProviderEntity entity = new ProviderEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.CompanyName = dt.Rows[i]["COMPANYNAME"].ToString();
                    entity.LegalCompanyName = dt.Rows[i]["LEGALCOMPANEYNAME"].ToString();
                    entity.Email = dt.Rows[i]["EMAIL"].ToString();
                    entity.PhoneNumber = dt.Rows[i]["PHONENUMBER"].ToString();                    
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

        public void CreateProvider(ProviderEntity providerEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO PROVIDER VALUES(@COMPANYNAME,
                                                                                        @LEGALCOMPANEYNAME,
                                                                                        @EMAIL,
                                                                                        @PHONENUMBER,                                                                                         
                                                                                        @CNPJ,
                                                                                        @ADDRESS, 
                                                                                        @STREETNUMBER, 
                                                                                        @CEP, 
                                                                                        @ACTIVE, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@COMPANYNAME", SqlDbType.VarChar).Value = providerEntity.CompanyName;
                    command.Parameters.Add("@LEGALCOMPANEYNAME", SqlDbType.VarChar).Value = providerEntity.LegalCompanyName;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = providerEntity.Email;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = providerEntity.PhoneNumber;                    
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = providerEntity.CNPJ;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = providerEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = providerEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = providerEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = providerEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProvider(ProviderEntity providerEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PROVIDER SET COMPANYNAME = @COMPANYNAME,
                                                                                       LEGALCOMPANEYNAME = @LEGALCOMPANEYNAME,
                                                                                       EMAIL = @EMAIL,
                                                                                       PHONENUMBER = @PHONENUMBER,                                                                                
                                                                                       CNPJ = @CNPJ,
                                                                                       [ADDRESS] = @ADDRESS,
                                                                                       STREETNUMBER = @STREETNUMBER,
                                                                                       CEP = @CEP,
                                                                                       ACTIVE = @ACTIVE
                                                                    WHERE ID = @PROVIDERID", con))
                {

                    command.Parameters.Add("@PROVIDERID", SqlDbType.Int).Value = providerEntity.Id;
                    command.Parameters.Add("@COMPANYNAME", SqlDbType.VarChar).Value = providerEntity.CompanyName;
                    command.Parameters.Add("@LEGALCOMPANEYNAME", SqlDbType.VarChar).Value = providerEntity.LegalCompanyName;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = providerEntity.Email;
                    command.Parameters.Add("@PHONENUMBER", SqlDbType.VarChar).Value = providerEntity.PhoneNumber;
                    command.Parameters.Add("@CNPJ", SqlDbType.VarChar).Value = providerEntity.CNPJ;
                    command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = providerEntity.Address;
                    command.Parameters.Add("@STREETNUMBER", SqlDbType.VarChar).Value = providerEntity.StreetNumber;
                    command.Parameters.Add("@CEP", SqlDbType.VarChar).Value = providerEntity.CEP;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = providerEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ProviderEntity> GetAllProvidersResumeList()
        {
            List<ProviderEntity> result = new List<ProviderEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   ID,
                                                                      COMPANYNAME,
                                                                      LEGALCOMPANEYNAME,
                                                                      CNPJ
                                                             FROM [PROVIDER]
                                                             WHERE ACTIVE = 1", con))
                {
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProviderEntity entity = new ProviderEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.CompanyName = dt.Rows[i]["COMPANYNAME"].ToString();
                    entity.LegalCompanyName = dt.Rows[i]["LEGALCOMPANEYNAME"].ToString();
                    entity.CNPJ = dt.Rows[i]["CNPJ"].ToString();

                    result.Add(entity);
                }

            }

            return result;
        }
    }
}
