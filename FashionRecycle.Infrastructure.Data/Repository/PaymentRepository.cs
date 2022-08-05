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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;

        public PaymentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PaymentsEntity GetPaymentById(int paymentId)
        {
            PaymentsEntity result = new PaymentsEntity();            
            PaymenyTypeEntity paymenyType = new PaymenyTypeEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.NAME,
                                                                     A.IDPAYMENTTYPE,
                                                                     B.DESCRIPTION,
                                                                     A.AMOUNT,
                                                                     A.PAYMENTDATE,
                                                                     A.PAYMENTMADE
                                                            FROM PAYMENTS A                                                           
                                                            INNER JOIN PAYMENTTYPE B
                                                            ON A.IDPAYMENTTYPE = B.ID
                                                            WHERE A.ID = @PAYMENTID AND A.ACTIVE = 1", con))
                {
                    command.Parameters.Add("@PAYMENTID", SqlDbType.Int).Value = paymentId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Name = dt.Rows[0]["NAME"].ToString();
                result.Amount = double.Parse(dt.Rows[0]["AMOUNT"].ToString());
                result.PaymentMade = bool.Parse(dt.Rows[0]["PAYMENTMADE"].ToString());
                result.PaymentDate = DateTime.Parse(dt.Rows[0]["PAYMENTDATE"].ToString());                    

                paymenyType.Id = int.Parse(dt.Rows[0]["IDPAYMENTTYPE"].ToString());
                paymenyType.Description = dt.Rows[0]["DESCRIPTION"].ToString();

                result.PaymenyType = paymenyType;

            }

            return result;
        }

        public List<PaymentsEntity> GetPaymentAll(string inicialDate, string finalDate)
        {
            List<PaymentsEntity> result = new List<PaymentsEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,                                                                     
                                                                     A.NAME,                                                                                                                                   
                                                                     A.IDPAYMENTTYPE,
                                                                     B.DESCRIPTION,
                                                                     A.AMOUNT,
                                                                     A.PAYMENTDATE,
                                                                     A.PAYMENTMADE,
                                                                     A.ACTIVE                                                                       
                                                            FROM PAYMENTS A                                                           
                                                            INNER JOIN PAYMENTTYPE B
                                                            ON A.IDPAYMENTTYPE = B.ID
                                                             WHERE CAST(A.PAYMENTDATE AS DATE) BETWEEN   CAST(@INICIALDATE AS DATE) AND CAST(@FINALDATE AS DATE) AND A.ACTIVE = 1", con))
                {
                    command.Parameters.Add("@INICIALDATE", SqlDbType.DateTime).Value = inicialDate == "" ? DBNull.Value : DateTime.Parse(inicialDate, CultureInfo.InvariantCulture);
                    command.Parameters.Add("@FINALDATE", SqlDbType.DateTime).Value = finalDate == "" ? DBNull.Value : DateTime.Parse(finalDate, CultureInfo.InvariantCulture);
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PaymentsEntity payments = new PaymentsEntity();                    
                    PaymenyTypeEntity paymenyType = new PaymenyTypeEntity();

                    payments.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    payments.Name = dt.Rows[i]["NAME"].ToString();
                    payments.Amount = double.Parse(dt.Rows[i]["AMOUNT"].ToString());
                    payments.PaymentDate = DateTime.Parse(dt.Rows[i]["PAYMENTDATE"].ToString());
                    payments.Active = bool.Parse(dt.Rows[i]["ACTIVE"].ToString());
                    payments.PaymentMade = bool.Parse(dt.Rows[i]["PAYMENTMADE"].ToString());

                    paymenyType.Id = int.Parse(dt.Rows[i]["IDPAYMENTTYPE"].ToString());
                    paymenyType.Description = dt.Rows[i]["DESCRIPTION"].ToString();
                    
                    payments.PaymenyType = paymenyType;

                    result.Add(payments);
                }

            }

            return result;
        }

        public void CreatePayment(PaymentsEntity paymentsEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"INSERT INTO PAYMENTS VALUES(@NAME,
                                                                                        @IDPAYMENTTYPE,
                                                                                        @AMOUNT,
                                                                                        @PAYMENTDATE,                                                                                                                                                                                
                                                                                        1, 
                                                                                        GETDATE(), 0)", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = paymentsEntity.Name == "" ? DBNull.Value : paymentsEntity.Name;
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.Int).Value = paymentsEntity.PaymenyType.Id;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = paymentsEntity.Amount;
                    command.Parameters.Add("@PAYMENTDATE", SqlDbType.DateTime).Value = paymentsEntity.PaymentDate;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePayment(PaymentsEntity paymentsEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PAYMENTS SET NAME = @NAME,                                                                                
                                                                                       IDPAYMENTTYPE = @IDPAYMENTTYPE,
                                                                                       AMOUNT = @AMOUNT,
                                                                                       PAYMENTDATE = @PAYMENTDATE,
                                                                                       PAYMENTMADE = @PAYMENTMADE
                                                                    WHERE ID = @IDPAYMENT", con))
                {

                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = paymentsEntity.Name == "" ? DBNull.Value : paymentsEntity.Name;
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.VarChar).Value = paymentsEntity.PaymenyType.Id;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = paymentsEntity.Amount;
                    command.Parameters.Add("@PAYMENTDATE", SqlDbType.DateTime).Value = paymentsEntity.PaymentDate;                    
                    command.Parameters.Add("@IDPAYMENT", SqlDbType.Int).Value = paymentsEntity.Id;
                    command.Parameters.Add("@PAYMENTMADE", SqlDbType.Bit).Value = paymentsEntity.PaymentMade;
                    command.ExecuteNonQuery();
                }
            }
        }

        public double GetMargin()
        {            

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT MARGIN FROM MARGIN", con))
                {                    
                    dt.Load(command.ExecuteReader());
                }

            }

            double result = 0;

            if (dt.Rows.Count > 0)
            {

                result = double.Parse(dt.Rows[0]["MARGIN"].ToString());
            }

            return result;
        }

        public void DeletePayment(int idPayment)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PAYMENTS SET ACTIVE = 0                                                                                                                                                                                                                                                          
                                                                    WHERE ID = @IDPAYMENT", con))
                {
                    
                    command.Parameters.Add("@IDPAYMENT", SqlDbType.Int).Value = idPayment;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
