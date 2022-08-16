using FashionRecycle.API.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycleJobs.Infrastructure.Repository
{
    public class PaymentRepository
    {
        public List<PaymentsEntity> GetPaymentAllActive()
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
                                                                     A.RECURRINGPATMENT
                                                            FROM PAYMENTS
                                                            WHERE ACTIVE = 1", con))
                {
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
                    payments.RecurringPayment = bool.Parse(dt.Rows[i]["RECURRINGPATMENT"].ToString());

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
                                                                                        GETDATE(), 0, @RECURRINGPATMENT)", con))
                {
                    command.Parameters.Add("@NAME", SqlDbType.VarChar).Value = paymentsEntity.Name == "" ? DBNull.Value : paymentsEntity.Name;
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.Int).Value = paymentsEntity.PaymenyType.Id;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = paymentsEntity.Amount;
                    command.Parameters.Add("@PAYMENTDATE", SqlDbType.DateTime).Value = paymentsEntity.PaymentDate;
                    command.Parameters.Add("@RECURRINGPATMENT", SqlDbType.Bit).Value = paymentsEntity.RecurringPayment;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
