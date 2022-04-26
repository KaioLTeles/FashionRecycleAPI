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
            PartnerEntity partnerEntity = new PartnerEntity(); 
            ProviderEntity providerEntity = new ProviderEntity();
            PaymenyTypeEntity paymenyType = new PaymenyTypeEntity();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.IDPARTNER,
                                                                     B.NAME,
                                                                     A.IDPROVIDER,
                                                                     C.COMPANYNAME,
                                                                     C.LEGALCOMPANEYNAME,
                                                                     C.CNPJ,
                                                                     A.IDPAYMENTTYPE,
                                                                     D.DESCRIPTION,
                                                                     A.AMOUNT,
                                                                     A.PAYMENTDATE,
                                                                     A.ACTIVE,
                                                                     A.CREATIONDATE
                                                            FROM PAYMENTS A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID
                                                            INNER JOIN [PROVIDER] C
                                                            ON A.IDPROVIDER = C.ID
                                                            INNER JOIN PAYMENTTYPE D
                                                            ON A.IDPAYMENTTYPE = D.ID
                                                            WHERE A.ID = @PAYMENTID", con))
                {
                    command.Parameters.Add("@PAYMENTID", SqlDbType.Int).Value = paymentId;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                result.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                result.Amount = double.Parse(dt.Rows[0]["AMOUNT"].ToString());
                result.PaymentDate = DateTime.Parse(dt.Rows[0]["PAYMENTDATE"].ToString());
                result.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                result.CreationDate = DateTime.Parse(dt.Rows[0]["CREATIONDATE"].ToString());

                partnerEntity.Id = int.Parse(dt.Rows[0]["IDPARTNER"].ToString());
                partnerEntity.Name = dt.Rows[0]["NAME"].ToString();

                providerEntity.Id = int.Parse(dt.Rows[0]["IDPROVIDER"].ToString());
                providerEntity.CompanyName = dt.Rows[0]["COMPANYNAME"].ToString();
                providerEntity.LegalCompanyName = dt.Rows[0]["LEGALCOMPANEYNAME"].ToString();
                providerEntity.CNPJ = dt.Rows[0]["CNPJ"].ToString();

                paymenyType.Id = int.Parse(dt.Rows[0]["IDPAYMENTTYPE"].ToString());
                paymenyType.Description = dt.Rows[0]["DESCRIPTION"].ToString();

                result.Partner = partnerEntity;
                result.Provider = providerEntity;
                result.PaymenyType = paymenyType;

            }

            return result;
        }

        public List<PaymentsEntity> GetPaymentAll(int paymentId, int idProvider, int idPartner)
        {
            List<PaymentsEntity> result = new List<PaymentsEntity>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.IDPARTNER,
                                                                     B.NAME,
                                                                     A.IDPROVIDER,
                                                                     C.COMPANYNAME,                                                                    
                                                                     A.IDPAYMENTTYPE,
                                                                     D.DESCRIPTION,
                                                                     A.AMOUNT,
                                                                     A.PAYMENTDATE
                                                                     A.ACTIVE
                                                            FROM PAYMENTS A
                                                            INNER JOIN [PARTNER] B
                                                            ON A.IDPARTNER = B.ID
                                                            INNER JOIN [PROVIDER] C
                                                            ON A.IDPROVIDER = C.ID
                                                            INNER JOIN PAYMENTTYPE D
                                                            ON A.IDPAYMENTTYPE = D.ID
                                                             WHERE (@IDPAYMENT IS NULL OR A.ID = @IDPAYMENT)
                                                             AND (@IDPARTNER IS NULL OR A.IDPARTNER = @IDPARTNER)
                                                             AND (@IDPROVIDER IS NULL OR A.IDPARTNER = @IDPROVIDER)", con))
                {
                    command.Parameters.Add("@IDPAYMENT", SqlDbType.Int).Value = paymentId == 0 ? DBNull.Value : paymentId;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.Int).Value = idPartner == 0 ? DBNull.Value : idPartner;
                    command.Parameters.Add("@IDPROVIDER", SqlDbType.Int).Value = idProvider == 0 ? DBNull.Value : idProvider;
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PaymentsEntity payments = new PaymentsEntity();
                    PartnerEntity partnerEntity = new PartnerEntity();
                    ProviderEntity providerEntity = new ProviderEntity();
                    PaymenyTypeEntity paymenyType = new PaymenyTypeEntity();

                    payments.Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    payments.Amount = double.Parse(dt.Rows[0]["AMOUNT"].ToString());
                    payments.PaymentDate = DateTime.Parse(dt.Rows[0]["PAYMENTDATE"].ToString());
                    payments.Active = bool.Parse(dt.Rows[0]["ACTIVE"].ToString());
                    

                    partnerEntity.Id = int.Parse(dt.Rows[0]["IDPARTNER"].ToString());
                    partnerEntity.Name = dt.Rows[0]["NAME"].ToString();

                    providerEntity.Id = int.Parse(dt.Rows[0]["IDPROVIDER"].ToString());
                    providerEntity.CompanyName = dt.Rows[0]["COMPANYNAME"].ToString();

                    paymenyType.Id = int.Parse(dt.Rows[0]["IDPAYMENTTYPE"].ToString());
                    paymenyType.Description = dt.Rows[0]["DESCRIPTION"].ToString();

                    payments.Partner = partnerEntity;
                    payments.Provider = providerEntity;
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
                using (SqlCommand command = new SqlCommand(@"INSERT INTO PAYMENTS VALUES(@IDPROVIDER, 
                                                                                        @IDPARTNER, 
                                                                                        @IDPAYMENTTYPE,
                                                                                        @AMOUNT,
                                                                                        @PAYMENTDATE,                                                                                                                                                                                
                                                                                        1, 
                                                                                        GETDATE())", con))
                {
                    command.Parameters.Add("@IDPROVIDER", SqlDbType.VarChar).Value = paymentsEntity.Provider.Id;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.VarChar).Value = paymentsEntity.Partner.Id;
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.Int).Value = paymentsEntity.PaymenyType.Id;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = paymentsEntity.Amount;
                    command.Parameters.Add("@PAYMENTDATE", SqlDbType.Decimal).Value = paymentsEntity.PaymentDate;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePayment(PaymentsEntity paymentsEntity)
        {
            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"UPDATE PAYMENTS SET IDPROVIDER = @IDPROVIDER,
                                                                                       IDPARTNER = @IDPARTNER,                                                                                       
                                                                                       IDPAYMENTTYPE = @IDPAYMENTTYPE,
                                                                                       AMOUNT = @AMOUNT,
                                                                                       PAYMENTDATE = @PAYMENTDATE,                                                                                                                                                                         
                                                                                       ACTIVE = @ACTIVE
                                                                    WHERE ID = @IDPAYMENT", con))
                {

                    command.Parameters.Add("@IDPROVIDER", SqlDbType.Int).Value = paymentsEntity.Provider.Id;
                    command.Parameters.Add("@IDPARTNER", SqlDbType.VarChar).Value = paymentsEntity.Partner.Id;
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.VarChar).Value = paymentsEntity.PaymenyType.Id;
                    command.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = paymentsEntity.Amount;
                    command.Parameters.Add("@PAYMENTDATE", SqlDbType.Decimal).Value = paymentsEntity.PaymentDate;                    
                    command.Parameters.Add("@IDPAYMENT", SqlDbType.Int).Value = paymentsEntity.Id;
                    command.Parameters.Add("@ACTIVE", SqlDbType.Bit).Value = paymentsEntity.Active == true ? 1 : 0;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
