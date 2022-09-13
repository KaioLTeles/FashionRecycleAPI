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
    public class SalesRepository : ISalesRepository
    {
        private readonly IConfiguration _configuration;

        public SalesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }       

        public int CreateSale(SalesEntity salesEntity, List<SalesItemsEntity> salesItemsEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();

                DataTable dt = new DataTable();

                var transaticon = con.BeginTransaction(IsolationLevel.RepeatableRead);

                int idSale = 0;

                try
                {
                    using (SqlCommand command = new SqlCommand(@"INSERT INTO SALES VALUES(@IDCLIENT,
                                                                                      @AMOUNTSALE,
                                                                                      @IDPAYMENTMETHOD,
                                                                                      @OBSERVATION,
                                                                                      1,
                                                                                      GETDATE(), @NUMBERINSTALLMENTS) SELECT TOP 1 ID FROM SALES ORDER BY [CREATIONDATE] DESC;", con, transaticon))
                    {
                        command.Parameters.Add("@IDCLIENT", SqlDbType.Int).Value = salesEntity.IdClient;
                        command.Parameters.Add("@AMOUNTSALE", SqlDbType.Decimal).Value = salesEntity.AmountSale;
                        command.Parameters.Add("@IDPAYMENTMETHOD", SqlDbType.Int).Value = salesEntity.IdPaymentMethod;
                        command.Parameters.Add("@OBSERVATION", SqlDbType.VarChar).Value = salesEntity.Observation;
                        command.Parameters.Add("@NUMBERINSTALLMENTS", SqlDbType.Int).Value = salesEntity.NumberInstallments;
                        dt.Load(command.ExecuteReader()); 
                    }

                    idSale = int.Parse(dt.Rows[0]["ID"].ToString());

                    if (idSale > 0)
                    {
                        foreach(var item in salesItemsEntity)
                        {
                            using (SqlCommand command = new SqlCommand(@"INSERT INTO SALESITEMS VALUES(@IDSALES,
                                                                                      @IDPRODUCT,
                                                                                      @IDPARTNER,
                                                                                      @AMOUNT,
                                                                                      @PRICESALE,
                                                                                      1,
                                                                                      GETDATE())", con, transaticon))
                            {
                                command.Parameters.Add("@IDSALES", SqlDbType.Int).Value = idSale;
                                command.Parameters.Add("@IDPRODUCT", SqlDbType.Int).Value = item.IdProduct;
                                command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = item.IdPartner;
                                command.Parameters.Add("@PRICESALE", SqlDbType.Decimal).Value = item.PriceSale;
                                command.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = item.Amount;
                                command.ExecuteNonQuery();
                            }
                        }                        

                        transaticon.Commit();
                        return idSale;
                    }
                    else{
                        transaticon.Rollback();
                        return 0;
                    }                    
                }
                catch(Exception)
                {
                    transaticon.Rollback();
                    throw;
                }
            }
        }
    }
}
