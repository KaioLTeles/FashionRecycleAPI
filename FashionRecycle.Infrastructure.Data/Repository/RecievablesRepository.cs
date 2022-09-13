using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Infrastructure.Data.Repository
{
    public class RecievablesRepository : IRecievablesRepository
    {
        private readonly IConfiguration _configuration;

        public RecievablesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<RecievableEntity> GetReciavableAll(string inicialDate, string finalDate, int idClient)
        {
            List<RecievableEntity> result = new List<RecievableEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,                                                                     
                                                                     A.NAME,                                                                                                                                   
                                                                     A.IDCLIENT,
                                                                     A.IDSALE,
                                                                     A.AMOUNT,
                                                                     A.SALEDATE,
                                                                     A.RECIEVEDATE,                                           
                                                                     A.STATUS,
                                                                     A.ACTIVE,
                                                                     A.CREATIONDATE,
                                                                     B.NAME AS NAMECLIENT
                                                            FROM RECEIVABLES A                                                           
                                                            INNER JOIN CLIENT B
                                                            ON A.IDCLIENT = B.ID
                                                             WHERE CAST(A.RECIEVEDATE AS DATE) BETWEEN   CAST(@INICIALDATE AS DATE) AND CAST(@FINALDATE AS DATE) AND A.ACTIVE = 1 AND (@IDCLIENT = 0 OR A.IDCLIENT = @IDCLIENT)", con))
                {
                    command.Parameters.Add("@INICIALDATE", SqlDbType.DateTime).Value = inicialDate == "" ? DBNull.Value : DateTime.Parse(inicialDate, CultureInfo.InvariantCulture);
                    command.Parameters.Add("@FINALDATE", SqlDbType.DateTime).Value = finalDate == "" ? DBNull.Value : DateTime.Parse(finalDate, CultureInfo.InvariantCulture);
                    command.Parameters.Add("@IDCLIENT", SqlDbType.Int).Value = idClient;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RecievableEntity recievable = new RecievableEntity();

                    recievable.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    recievable.Name = dt.Rows[i]["NAME"].ToString();
                    recievable.Amout = double.Parse(dt.Rows[i]["AMOUNT"].ToString());
                    recievable.RecieveDate = DateTime.Parse(dt.Rows[i]["RECIEVEDATE"].ToString());
                    recievable.SaleDate = DateTime.Parse(dt.Rows[i]["SALEDATE"].ToString());
                    recievable.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());
                    recievable.Active = bool.Parse(dt.Rows[i]["ACTIVE"].ToString());
                    recievable.Status = bool.Parse(dt.Rows[i]["STATUS"].ToString());
                    recievable.ClientName = dt.Rows[i]["NAMECLIENT"].ToString();

                    recievable.SaleDateFormated = recievable.SaleDate.ToString("dd/MM/yyyy");
                    recievable.RecieveDateFormated = recievable.RecieveDate.ToString("dd/MM/yyyy");


                    result.Add(recievable);
                }

            }

            return result;
        }

        public RecievableEntity GetReciavableById(int idReceiable)
        {
            RecievableEntity result = new RecievableEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  ID,                                                                     
                                                                     NAME,                                                                                                                                   
                                                                     IDCLIENT,
                                                                     IDSALE,
                                                                     AMOUNT,
                                                                     SALEDATE,
                                                                     RECIEVEDATE,                                           
                                                                     STATUS,
                                                                     ACTIVE,
                                                                     CREATIONDATE                                                                     
                                                             FROM RECEIVABLES 
                                                             WHERE ID = @IDRECEIABLE", con))
                {
                    command.Parameters.Add("@IDRECEIABLE", SqlDbType.Int).Value = idReceiable;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {

                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Name = dt.Rows[0]["NAME"].ToString();
                result.Amout = double.Parse(dt.Rows[0]["AMOUNT"].ToString());
                result.RecieveDate = DateTime.Parse(dt.Rows[0]["RECIEVEDATE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.Status = bool.Parse(dt.Rows[0]["STATUS"].ToString());
            }

            return result;
        }

        public void CreateRecievable(RecievableEntity recievableEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO RECEIVABLES VALUES(@NAME,
                                                                                        @IDCLIENT,
                                                                                        @IDSALE,
                                                                                        @AMOUNT,
                                                                                        @SALEDATE,
                                                                                        @RECIEVEDATE,
                                                                                        0,                                                                                                                                                                                                                                                                        
                                                                                        1, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = recievableEntity.Name == "" ? DBNull.Value : recievableEntity.Name;
                    command.Parameters.Add("@IDCLIENT", SqlDbType.Int).Value = recievableEntity.IdClient;
                    command.Parameters.Add("@IDSALE", SqlDbType.Int).Value = recievableEntity.IdSale;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = recievableEntity.Amout;
                    command.Parameters.Add("@SALEDATE", SqlDbType.DateTime).Value = recievableEntity.SaleDate;
                    command.Parameters.Add("@RECIEVEDATE", SqlDbType.DateTime).Value = recievableEntity.RecieveDate;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateReceivableToPaid(int idReceivable)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE RECEIVABLES SET STATUS = 1
                                                                    WHERE ID = @IDRECEIVABLE", con))
                {
                    command.Parameters.Add("@IDRECEIVABLE", SqlDbType.Int).Value = idReceivable;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateReceivableToUnpaid(int idReceivable)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE RECEIVABLES SET STATUS = 0
                                                                    WHERE ID = @IDRECEIVABLE", con))
                {
                    command.Parameters.Add("@IDRECEIVABLE", SqlDbType.Int).Value = idReceivable;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
