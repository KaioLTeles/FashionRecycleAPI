using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet("getListProductAll")]
        public IActionResult GetListProductAll(string id, int idBrand, int idPartner)
        {
            var input = new ListAllProductsInputModel
            {
                Id = id == null ? "" : id,                
                IdBrand = idBrand,
                IdPartner = idPartner

            };

            try
            {
                var result = _productBusiness.GetListProductAll(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de produtos - ", ex);
                return BadRequest("Erro ao buscar a lista de produtos favor contactar a TI");
            }
        }

        [HttpGet("getProductById")]
        public IActionResult GetProductById(int idProduct)
        {
            try
            {
                var result = _productBusiness.GetProductById(idProduct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do produto - ", ex);
                return BadRequest("Erro ao buscar os dados do produto favor contactar a TI");
            }
        }

        [HttpPost("alterOrCreateProduct")]
        public IActionResult AlterOrCreateProduct([FromBody] CreateProductInputModel inputModel)
        {
            try
            {
                _productBusiness.AlterOrCreateProduct(inputModel);
                return Ok("Produto processado com sucesso!");
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/alterar o produto " + inputModel.id + " - ", ex);
                return BadRequest("Erro ao criar/alterar o produto favor contactar a TI");
            }
        }

        [HttpGet("getProductAllForSale")]
        public IActionResult GetProductAllForSale()
        {
            try
            {
                var result = _productBusiness.GetProductAllForSale();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os produtos disponiveis para venda - ", ex);
                return BadRequest("Erro ao buscar os produtos disponiveis para venda favor contactar a TI");
            }
        }


        [HttpGet("getProductByPartnerForSale")]
        public IActionResult GetProductByPartnerForSale(int idPartner)
        {
            try
            {
                var result = _productBusiness.GetProductByPartnerForSale(idPartner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os produtos disponiveis para venda - ", ex);
                return BadRequest("Erro ao buscar os produtos disponiveis para venda favor contactar a TI");
            }
        }

    }
}
