using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Enums;
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
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProductEntity GetProductById(int productId)
        {
            ProductEntity result = new ProductEntity();
            PartnerEntity partnerEntity = new PartnerEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.[NAME],
                                                                     A.BRAND,
                                                                     A.AMOUNTINVENTORY,
                                                                     A.PRICEPARTNER,
                                                                     A.PRICESALE,
                                                                     A.IDPARTNER,
                                                                     B.[NAME] AS NAMEPARTNER,
                                                                     B.PHONENUMBER,
                                                                     B.CPF,
                                                                     B.CNPJ,
                                                                     B.ADDRESS,
                                                                     B.STREETNUMBER,
                                                                     B.CEP,
                                                                     B.ACTIVE AS ACTIVEPARTNER,
                                                                     B.CREATIONDATE AS CREATIONDATEPARTNER,
                                                                     A.ACTIVE,
                                                                     A.CREATIONDATE,
                                                                     A.ALTERNATIVE_ID,
                                                                     A.SERIALNUMBER,
                                                                     A.MODEL,
                                                                     A.COLOUR,
                                                                     A.OBSERVATION,
                                                                     A.BRANDID,
                                                                     A.PRODUCTSTATUS
                                                            FROM PRODUCT A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID
                                                            WHERE A.ID = @PRODUCTID", con))
                {
                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = productId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Name = dt.Rows[0]["NAME"].ToString();
                result.Brand = dt.Rows[0]["BRAND"].ToString();
                result.AmountInventory = int.Parse(dt.Rows[0]["AMOUNTINVENTORY"].ToString());
                result.PricePartner = double.Parse(dt.Rows[0]["PRICEPARTNER"].ToString());
                result.PriceSale = double.Parse(dt.Rows[0]["PRICESALE"].ToString());                
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());
                result.BrandId = int.Parse(dt.Rows[0]["BRANDID"].ToString());
                result.ProductStatus = int.Parse(dt.Rows[0]["PRODUCTSTATUS"].ToString());
                result.AlternativeId = dt.Rows[0]["ALTERNATIVE_ID"].ToString();
                result.SerialNumber = dt.Rows[0]["SERIALNUMBER"].ToString();
                result.Model = dt.Rows[0]["MODEL"].ToString();
                result.Colour = dt.Rows[0]["COLOUR"].ToString();
                result.Observation = dt.Rows[0]["OBSERVATION"].ToString();

                partnerEntity.Id = int.Parse(dt.Rows[0]["IDPARTNER"].ToString());
                partnerEntity.Name = dt.Rows[0]["NAMEPARTNER"].ToString();
                partnerEntity.PhoneNumber = dt.Rows[0]["PHONENUMBER"].ToString();
                partnerEntity.CPF = dt.Rows[0]["CPF"].ToString();
                partnerEntity.CNPJ = dt.Rows[0]["CNPJ"].ToString();
                partnerEntity.Address = dt.Rows[0]["ADDRESS"].ToString();
                partnerEntity.StreetNumber = dt.Rows[0]["STREETNUMBER"].ToString();
                partnerEntity.CEP = dt.Rows[0]["CEP"].ToString();
                partnerEntity.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                partnerEntity.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATEPARTNER"].ToString());

                result.Partner = partnerEntity;

            }

            return result;
        }

        public List<ProductEntity> GetProductAll(string productId, int idBrand, int idPartner)
        {
            List<ProductEntity> result = new List<ProductEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.[NAME],                                                                                                                                          
                                                                     A.PRICEPARTNER,
                                                                     A.PRICESALE,
                                                                     A.IDPARTNER,
                                                                     B.[NAME] AS NAMEPARTNER,                                                                     
                                                                     A.ACTIVE,
                                                                     A.CREATIONDATE,
                                                                     A.BRANDID,
                                                                     C.NAME AS NAMEBRAND,
                                                                     A.ALTERNATIVE_ID,
                                                                     A.SERIALNUMBER,
                                                                     A.MODEL,
                                                                     A.COLOUR
                                                            FROM PRODUCT A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID
                                                            INNER JOIN [BRAND] C
                                                            ON A.BRANDID = C.ID
                                                             WHERE (@PRODUCTID IS NULL OR A.ALTERNATIVE_ID = @PRODUCTID)
                                                             AND (@IDPARTNER IS NULL OR A.IDPARTNER = @IDPARTNER)
                                                             AND (@IDBRAND IS NULL OR A.BRANDID = @IDBRAND)", con))
                {
                    command.Parameters.Add("@PRODUCTID", SqlDbType.VarChar).Value = productId == "" ? DBNull.Value : productId;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = idPartner == 0 ? DBNull.Value : idPartner;
                    command.Parameters.Add("@IDBRAND", SqlDbType.Int).Value = idBrand == 0 ? DBNull.Value : idBrand;                    
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductEntity entity = new ProductEntity();
                    PartnerEntity partnerEntity = new PartnerEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.BrandId = int.Parse(dt.Rows[i]["BRANDID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();
                    entity.AlternativeId = dt.Rows[i]["ALTERNATIVE_ID"].ToString();
                    entity.SerialNumber = dt.Rows[i]["SERIALNUMBER"].ToString();
                    entity.Model = dt.Rows[i]["MODEL"].ToString();
                    entity.Colour = dt.Rows[i]["COLOUR"].ToString();
                    entity.Brand = dt.Rows[i]["NAMEBRAND"].ToString();
                    entity.PricePartner = double.Parse(dt.Rows[i]["PRICEPARTNER"].ToString());
                    entity.PriceSale = double.Parse(dt.Rows[i]["PRICESALE"].ToString());
                    entity.Active = bool.Parse(dt.Rows[i]["ACTIVE"].ToString());
                    entity.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());

                    partnerEntity.Id = int.Parse(dt.Rows[i]["IDPARTNER"].ToString());
                    partnerEntity.Name = dt.Rows[i]["NAMEPARTNER"].ToString();

                    entity.Partner = partnerEntity;

                    result.Add(entity);
                }

            }

            return result;
        }

        public void CreateProduct(ProductEntity productEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO PRODUCT VALUES(@NAME, 
                                                                                        NULL, 
                                                                                        1,
                                                                                        @PRICEPARTNER,
                                                                                        @PRICESALE, 
                                                                                        @IDPARTNER,                                                                                         
                                                                                        @ACTIVE, 
                                                                                        GETDATE(),
                                                                                        @PRODUCTSTATUS,
                                                                                        @SERIALNUMBER,
                                                                                        NULL,
                                                                                        @MODEL,
                                                                                        @COLOUR,
                                                                                        @OBSERVATION,
                                                                                        @ALTERNATIVE_ID,
                                                                                        @BRANDID)", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = productEntity.Name;                                 
                    command.Parameters.Add("@PRICEPARTNER", SqlDbType.Decimal).Value = productEntity.PricePartner;
                    command.Parameters.Add("@PRICESALE", SqlDbType.Decimal).Value = productEntity.PriceSale;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = productEntity.Partner.Id;                                        
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = productEntity.Active == true ? 1 : 0;
                    command.Parameters.Add("@PRODUCTSTATUS", SqlDbType.Int).Value = productEntity.ProductStatus;
                    command.Parameters.Add("@SERIALNUMBER", SqlDbType.VarChar).Value = productEntity.SerialNumber;
                    command.Parameters.Add("@COLOUR", SqlDbType.VarChar).Value = productEntity.Colour;
                    command.Parameters.Add("@OBSERVATION", SqlDbType.VarChar).Value = productEntity.Observation;
                    command.Parameters.Add("@ALTERNATIVE_ID", SqlDbType.VarChar).Value = productEntity.AlternativeId;
                    command.Parameters.Add("@MODEL", SqlDbType.VarChar).Value = productEntity.Model;
                    command.Parameters.Add("@BRANDID", SqlDbType.Int).Value = productEntity.BrandId;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(ProductEntity productEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PRODUCT SET NAME = @NAME,                                                                                                                                                                                                                                                     
                                                                                       PRICEPARTNER = @PRICEPARTNER,
                                                                                       PRICESALE = @PRICESALE,
                                                                                       IDPARTNER = @IDPARTNER,                                                                                       
                                                                                       ACTIVE = @ACTIVE,
                                                                                       PRODUCTSTATUS = @PRODUCTSTATUS,
                                                                                       BRANDID = @BRANDID,
                                                                                       SERIALNUMBER = @SERIALNUMBER,                                                                                        
                                                                                       MODEL = @MODEL,
                                                                                       COLOUR = @COLOUR,
                                                                                       OBSERVATION = @OBSERVATION                                                                                    
                                                                    WHERE ID = @PRODUCTID", con))
                {

                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = productEntity.Id;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = productEntity.Name;                    
                    command.Parameters.Add("@PRICEPARTNER", SqlDbType.Decimal).Value = productEntity.PricePartner;
                    command.Parameters.Add("@PRICESALE", SqlDbType.Decimal).Value = productEntity.PriceSale;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = productEntity.Partner.Id;                   
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = productEntity.Active == true ? 1 : 0;
                    command.Parameters.Add("@PRODUCTSTATUS", SqlDbType.Int).Value = productEntity.ProductStatus;
                    command.Parameters.Add("@BRANDID", SqlDbType.Int).Value = productEntity.BrandId;
                    command.Parameters.Add("@SERIALNUMBER", SqlDbType.VarChar).Value = productEntity.SerialNumber;
                    command.Parameters.Add("@COLOUR", SqlDbType.VarChar).Value = productEntity.Colour;
                    command.Parameters.Add("@OBSERVATION", SqlDbType.VarChar).Value = productEntity.Observation;
                    command.Parameters.Add("@MODEL", SqlDbType.VarChar).Value = productEntity.Model;                    
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ProductEntity> GetProductAllForSale()
        {
            List<ProductEntity> result = new List<ProductEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.[NAME],
                                                                     A.BRAND,                                                                                                                                          
                                                                     A.PRICESALE,
                                                                     A.IDPARTNER,
                                                                     B.[NAME] AS NAMEPARTNER,
                                                                     A.AMOUNTINVENTORY,
                                                                     A.ALTERNATIVE_ID,
                                                                     A.MODEL
                                                            FROM PRODUCT A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID                                                        
                                                            WHERE A.ACTIVE = 1 AND A.PRODUCTSTATUS = 1", con))
                {              
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductEntity entity = new ProductEntity();
                    PartnerEntity partnerEntity = new PartnerEntity();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.Name = dt.Rows[i]["NAME"].ToString();
                    entity.Brand = dt.Rows[i]["BRAND"].ToString();
                    entity.PriceSale = double.Parse(dt.Rows[i]["PRICESALE"].ToString());
                    entity.AmountInventory = int.Parse(dt.Rows[i]["AMOUNTINVENTORY"].ToString());
                    entity.AlternativeId = dt.Rows[i]["ALTERNATIVE_ID"].ToString();
                    entity.Model = dt.Rows[i]["MODEL"].ToString();

                    partnerEntity.Id = int.Parse(dt.Rows[i]["IDPARTNER"].ToString());
                    partnerEntity.Name = dt.Rows[i]["NAMEPARTNER"].ToString();

                    entity.Partner = partnerEntity;

                    entity.Name = entity.AlternativeId + "-" + entity.Name + "-" + entity.Model;

                    result.Add(entity);
                }

            }

            return result;
        }

        public void UpdateProductStatus(int idProduct)
        {

            int productNewStatus = (int)ProductStatusEnum.sold;


            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PRODUCT SET PRODUCTSTATUS = @PRODUCTSTATUS
                                                                    WHERE ID = @PRODUCTID", con))
                {

                    command.Parameters.Add("@PRODUCTSTATUS", SqlDbType.Int).Value = productNewStatus;
                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = idProduct;
                    command.ExecuteNonQuery();
                }
            }
        }

        public int CoutPartnerPorducts(int partnerId)
        {            
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT *
                                                            FROM PRODUCT                                                   
                                                            WHERE IDPARTNER = @IDPARTNER", con))
                {
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = partnerId;
                    dt.Load(command.ExecuteReader());
                }

            }

            return dt.Rows.Count;
        }
    }
}
