using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
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
    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration _configuration;

        public ReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ReportAllSalesResumedViewModel> GetAllSalesResumed(ReportSalesInputModel inputModel)
        {
            List<ReportAllSalesResumedViewModel> result = new List<ReportAllSalesResumedViewModel>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT  A.ID,
                                                                     A.IDCLIENT,
                                                                     B.NAME, 
                                                                     A.AMOUNTSALE,
                                                                     A.IDPAYMENTMETHOD, 
                                                                     C.DESCRIPTION,                                                                      
                                                                     A.CREATIONDATE,
                                                                     D.IDPARTNER,
                                                                     A.ACTIVE,
                                                                     D.IDPRODUCT,
                                                                     E.NAME AS NAMEPRODUCT,
                                                                     E.ALTERNATIVE_ID
                                                             FROM SALES A 
                                                             INNER JOIN CLIENT B 
                                                             ON A.IDCLIENT = B.ID
                                                             INNER JOIN PAYMENTMETHOD C 
                                                             ON A.IDPAYMENTMETHOD = C.ID
                                                             INNER JOIN SALESITEMS D 
                                                             ON A.ID = D.IDSALES
                                                             INNER JOIN PRODUCT E 
                                                             ON E.ID = D.IDPRODUCT
                                                             WHERE (@ID IS NULL OR A.ID = @ID) 
                                                             AND (@CLIENTID IS NULL OR A.IDCLIENT = @CLIENTID)                                                              
                                                             AND (@PARTNERID IS NULL OR  D.IDPARTNER = @PARTNERID)
                                                             AND (@BRANDID IS NULL OR  E.BRANDID = @BRANDID)                                                           
                                                             AND (A.CREATIONDATE BETWEEN   @INICIALDATE AND @FINALDATE) AND A.ACTIVE = 1
                                                             ORDER BY A.CREATIONDATE,A.ID,D.ID", con))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = inputModel.idSale == 0 ? DBNull.Value : inputModel.idSale;
                    command.Parameters.Add("@CLIENTID", SqlDbType.Int).Value = inputModel.idClient == 0 ? DBNull.Value : inputModel.idClient;                    
                    command.Parameters.Add("@PARTNERID", SqlDbType.Int).Value = inputModel.idPartner == 0 ? DBNull.Value : inputModel.idPartner;
                    command.Parameters.Add("@BRANDID", SqlDbType.VarChar).Value = inputModel.brand == 0 ? DBNull.Value : inputModel.idPartner;
                    command.Parameters.Add("@INICIALDATE", SqlDbType.DateTime).Value = inputModel.inicialFilterDate == "" ? DBNull.Value : DateTime.Parse(inputModel.inicialFilterDate, CultureInfo.InvariantCulture);
                    command.Parameters.Add("@FINALDATE", SqlDbType.DateTime).Value = inputModel.finalFilterDate == "" ? DBNull.Value : DateTime.Parse(inputModel.finalFilterDate, CultureInfo.InvariantCulture);
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ReportAllSalesResumedViewModel entity = new ReportAllSalesResumedViewModel();


                    entity.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                    entity.NameClient = dt.Rows[i]["NAME"].ToString();
                    entity.ProductDesciption = dt.Rows[i]["NAMEPRODUCT"].ToString();
                    entity.AlternativeId = dt.Rows[i]["ALTERNATIVE_ID"].ToString();
                    entity.AmountSale = double.Parse(dt.Rows[i]["AMOUNTSALE"].ToString());
                    entity.PaymentMethod = dt.Rows[i]["DESCRIPTION"].ToString();                    
                    entity.Active = bool.Parse(dt.Rows[i]["ACTIVE"].ToString());
                    entity.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());

                    entity.CreationDateFormated = entity.CreationDate.ToString("dd/MM/yyyy");

                    result.Add(entity);
                }

            }

            return result;
        }

        public List<ReportAllPaymentsViewModel> GellAllPaymentsReport(string inicialDate, string finalDate, int idPaymentType)
        {
            List<ReportAllPaymentsViewModel> result = new List<ReportAllPaymentsViewModel>();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_configuration["ConnectionStrings:Default"]))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT   A.CREATIONDATE,
                                                                      A.NAME,
                                                                      B.[DESCRIPTION],
                                                                      A.AMOUNT,
                                                                      A.PAYMENTDATE,
                                                                      A.IDPAYMENTTYPE
                                                             FROM PAYMENTS A 
                                                             INNER JOIN PAYMENTTYPE B 
                                                             ON A.IDPAYMENTTYPE = B.ID
                                                             WHERE   A.ACTIVE = 1
                                                                     AND (A.PAYMENTDATE BETWEEN   @INICIALDATE AND @FINALDATE)
                                                                     AND (@IDPAYMENTTYPE IS NULL OR  A.IDPAYMENTTYPE = @IDPAYMENTTYPE)", con))
                {
                    command.Parameters.Add("@IDPAYMENTTYPE", SqlDbType.Int).Value = idPaymentType == 0 ? DBNull.Value : idPaymentType;
                    command.Parameters.Add("@INICIALDATE", SqlDbType.DateTime).Value = inicialDate == "" ? DBNull.Value : DateTime.Parse(inicialDate, CultureInfo.InvariantCulture);
                    command.Parameters.Add("@FINALDATE", SqlDbType.DateTime).Value = finalDate == "" ? DBNull.Value : DateTime.Parse(finalDate, CultureInfo.InvariantCulture);
                    dt.Load(command.ExecuteReader());
                }

            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ReportAllPaymentsViewModel entity = new ReportAllPaymentsViewModel();


                    entity.Name = dt.Rows[i]["NAME"].ToString();
                    entity.PaymentDescription = dt.Rows[i]["DESCRIPTION"].ToString();
                    entity.Amount = double.Parse(dt.Rows[i]["AMOUNT"].ToString());                                       
                    entity.CreationDate = DateTime.Parse(dt.Rows[i]["CREATIONDATE"].ToString());
                    entity.PaymentDate = DateTime.Parse(dt.Rows[i]["PAYMENTDATE"].ToString());

                    entity.PaymentDateFormated = entity.PaymentDate.ToString("dd/MM/yyyy");

                    entity.CreationDateFormated = entity.CreationDate.ToString("dd/MM/yyyy");

                    result.Add(entity);
                }

            }

            return result;
        }
    }
}
