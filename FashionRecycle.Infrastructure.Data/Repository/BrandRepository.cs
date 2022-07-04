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
    public class BrandRepository : IBrandRepository
    {
        private readonly IConfiguration _configuration;

        public BrandRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<BrandEntity> GetBrandAll()
        {
            List<BrandEntity> result = new List<BrandEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,
                                                                     [NAME]                                                                  
                                                            FROM [BRAND]
                                                            WHERE ACTIVE = 1", con))
                {                    
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BrandEntity entity = new BrandEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();                    

                    result.Add(entity);
                }

            }

            return result;
        }

        public void CreateBrand(BrandEntity brandEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO BRAND VALUES( @NAME,                                                                                        
                                                                                        @ACTIVE, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = brandEntity.Name;                   
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = brandEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBrand(BrandEntity brandEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE BRAND SET [NAME] = @NAME
                                                                    WHERE ID = @BRANDID", con))
                {

                    command.Parameters.Add("@BRANDID", SqlDbType.Int).Value = brandEntity.Id;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = brandEntity.Name;                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveBrand(int brandId)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE BRAND SET ACTIVE = 0
                                                                    WHERE ID = @BRANDID", con))
                {

                    command.Parameters.Add("@BRANDID", SqlDbType.Int).Value = brandId;                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool CheckStatusRemoveBrand(int brandId)
        {                        
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  *
                                                            FROM PRODUCT
                                                            WHERE BRANDID = @BRANDID", con))
                {
                    command.Parameters.Add("@BRANDID", SqlDbType.Int).Value = brandId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {

                return true; // Foi achado produtos com essa marca, então não pode deletar
            }
            else
            {
                return false; // Não foi achado produtos com essa marca, então pode deletar
            }
        }

    }
}
