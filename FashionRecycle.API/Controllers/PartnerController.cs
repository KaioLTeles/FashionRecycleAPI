using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FashionRecycle.Application.Utils;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerBusiness _partnerBusiness;

        public PartnerController(IPartnerBusiness partnerBusiness)
        {
            _partnerBusiness = partnerBusiness;
        }

        [HttpGet("getListPartnerAll")]
        public IActionResult GetListPartnerAll(int id, string name, string cpf, string cnpj)
        {
            var input = new ListAllPartnersInputModel
            {
                Id = id,
                Name = name == null ? String.Empty : name,
                CPF = cpf == null ? String.Empty : cpf,
                CNPJ = cnpj == null ? String.Empty : cnpj
            };

            try
            {
                var result = _partnerBusiness.GetListPartnerAll(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de Fornecedor - ", ex);
                return BadRequest("Erro ao buscar a lista de Fornecedor favor contactar a TI");
            }
        }

        [HttpGet("getPartnerById")]
        public IActionResult GetPartnerById(int idPartner)
        {
            try
            {
                var result = _partnerBusiness.GetPartnerById(idPartner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do Fornecedor - ", ex);
                return BadRequest("Erro ao buscar os dados do Fornecedor favor contactar a TI");
            }
        }

        [HttpPost("alterOrCreatePartner")]
        public IActionResult AlterOrCreatePartner([FromBody] CreatePartnerInputModel inputModel)
        {
            try
            {
                _partnerBusiness.AlterOrCreatePartner(inputModel);
                return Ok("Fornecedor processado com sucesso!");
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/alterar o Fornecedor " + inputModel.id + " - ", ex);
                return BadRequest("Erro ao criar/alterar o Fornecedor favor contactar a TI");
            }
        }

        [HttpGet("getAllPartnersResumeList")]
        public IActionResult GetAllPartnersResumeList()
        {
            try
            {
                var result = _partnerBusiness.GetAllPartnersResumeList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do Fornecedor - ", ex);
                return BadRequest("Erro ao buscar os dados do Fornecedor favor contactar a TI");
            }
        }
    }
}
