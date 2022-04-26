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
                                                                     A.CREATIONDATE
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

        public List<ProductEntity> GetProductAll(int productId, string brand, string name, int idPartner)
        {
            List<ProductEntity> result = new List<ProductEntity>();

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
                                                                     A.ACTIVE,
                                                                     A.CREATIONDATE
                                                            FROM PRODUCT A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID
                                                             WHERE (@PRODUCTID IS NULL OR A.ID = @PRODUCTID)
                                                             AND (@IDPARTNER IS NULL OR A.IDPARTNER = @IDPARTNER)
                                                             AND (@BRAND IS NULL OR @BRAND = '' OR A.BRAND LIKE '%'+@BRAND+'%')
                                                             AND (@NAME IS NULL OR @NAME = '' OR A.[NAME] LIKE '%'+@NAME+'%')", con))
                {
                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = productId == 0 ? DBNull.Value : productId;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = idPartner == 0 ? DBNull.Value : idPartner;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = brand;
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
                    entity.AmountInventory = int.Parse(dt.Rows[i]["AMOUNTINVENTORY"].ToString());
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
                                                                                        @BRAND, 
                                                                                        @AMOUNTINVENTORY,
                                                                                        @PRICEPARTNER,
                                                                                        @PRICESALE, 
                                                                                        @IDPARTNER,                                                                                         
                                                                                        @ACTIVE, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = productEntity.Name;
                    command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = productEntity.Brand;
                    command.Parameters.Add("@AMOUNTINVENTORY", SqlDbType.Int).Value = productEntity.AmountInventory;
                    command.Parameters.Add("@PRICEPARTNER", SqlDbType.Decimal).Value = productEntity.PricePartner;
                    command.Parameters.Add("@PRICESALE", SqlDbType.Decimal).Value = productEntity.PriceSale;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = productEntity.Partner.Id;                                        
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = productEntity.Active == true ? 1 : 0;
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
                                                                                       BRAND = @BRAND,                                                                                       
                                                                                       AMOUNTINVENTORY = @AMOUNTINVENTORY,
                                                                                       PRICEPARTNER = @PRICEPARTNER,
                                                                                       PRICESALE = @PRICESALE,
                                                                                       IDPARTNER = @IDPARTNER,                                                                                       
                                                                                       ACTIVE = @ACTIVE
                                                                    WHERE ID = @PRODUCTID", con))
                {

                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = productEntity.Id;
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = productEntity.Name;
                    command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = productEntity.Brand;
                    command.Parameters.Add("@AMOUNTINVENTORY", SqlDbType.Int).Value = productEntity.AmountInventory;
                    command.Parameters.Add("@PRICEPARTNER", SqlDbType.Decimal).Value = productEntity.PricePartner;
                    command.Parameters.Add("@PRICESALE", SqlDbType.Decimal).Value = productEntity.PriceSale;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = productEntity.Partner.Id;                   
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = productEntity.Active == true ? 1 : 0;
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
                                                                     A.AMOUNTINVENTORY
                                                            FROM PRODUCT A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID                                                        
                                                            WHERE A.ACTIVE = 1 AND A.AMOUNTINVENTORY > 0", con))
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

                    partnerEntity.Id = int.Parse(dt.Rows[i]["IDPARTNER"].ToString());
                    partnerEntity.Name = dt.Rows[i]["NAMEPARTNER"].ToString();

                    entity.Partner = partnerEntity;

                    result.Add(entity);
                }

            }

            return result;
        }

        public void UpdateProductAmount(int idProduct, int amount)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PRODUCT SET AMOUNTINVENTORY = AMOUNTINVENTORY - @AMOUNTINVENTORY
                                                                    WHERE ID = @PRODUCTID", con))
                {
                   
                    command.Parameters.Add("@AMOUNTINVENTORY", SqlDbType.Int).Value = amount;
                    command.Parameters.Add("@PRODUCTID", SqlDbType.Int).Value = idProduct;
                    command.ExecuteNonQuery();
                }
            }
        }        
    }
}
