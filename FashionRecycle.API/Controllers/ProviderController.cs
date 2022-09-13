using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderBusiness _providerBusiness;

        public ProviderController(IProviderBusiness providerBusiness)
        {
            _providerBusiness = providerBusiness;
        }

        [HttpGet("getListProviderAll")]
        public IActionResult GetListProviderAll(int id, string cnpj)
        {
            var input = new ListAllProvidersInputModel
            {
                Id = id,                
                CNPJ = cnpj == null ? String.Empty : cnpj
            };

            try
            {
                var result = _providerBusiness.GetListProviderAll(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de fornecedores - ", ex);
                return BadRequest("Erro ao buscar a lista de fornecedores favor contactar a TI");
            }
        }

        [HttpGet("getProviderById")]
        public IActionResult GetProviderById(int idProvider)
        {
            try
            {
                var result = _providerBusiness.GetProviderById(idProvider);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do fornecedor - ", ex);
                return BadRequest("Erro ao buscar os dados do fornecedor favor contactar a TI");
            }
        }

        [HttpPost("alterOrCreateProvider")]
        public IActionResult AlterOrCreateProvider([FromBody] CreateProviderInputModel inputModel)
        {
            try
            {
                _providerBusiness.AlterOrCreateProvider(inputModel);
                return Ok("Fornecedor processado com sucesso!");
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/alterar o fornecedor " + inputModel.id + " - ", ex);
                return BadRequest("Erro ao criar/alterar o fornecedor favor contactar a TI");
            }
        }

        [HttpGet("getAllProvidersResumeList")]
        public IActionResult GetAllProvidersResumeList()
        {
            try
            {
                var result = _providerBusiness.GetAllProvidersResumeList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados de fornecedor - ", ex);
                return BadRequest("Erro ao buscar os dados de fornecedor favor contactar a TI");
            }
        }
    }
}
