using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FashionRecycle.Application.Utils;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet("getListClientsAll")]
        public IActionResult GetClientAll(int idClient, string nameClient, string cpfClient, string cnpj)
        {
            var input = new ListAllClientsInputModel
            {
                Id = idClient,
                Name = nameClient == null ? String.Empty : nameClient,
                CPF = cpfClient == null ? String.Empty : cpfClient
            };

            try
            {
                var result = _clientBusiness.GetAllClientsList(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar a lista de clientes - ", ex);
                return BadRequest("Erro ao buscar a lista de clientes favor contactar a TI");
            }            
        }

        [HttpGet("getClientById")]
        public IActionResult GetClientById(int idClient)
        {            
            try
            {
                var result = _clientBusiness.GetClientById(idClient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do cliente - ", ex);
                return BadRequest("Erro ao buscar os dados do cliente favor contactar a TI");
            }
        }

        [HttpPost("alterOrCreateClient")]
        public IActionResult AlterOrCreateClient([FromBody] CreateClientInputModel inputModel)
        {
            try
            {
                _clientBusiness.AlterOrCreateClient(inputModel);
                return Ok("Cliente processado com sucesso!");
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar/alterar o cliente " + inputModel.id + " - ", ex);
                return BadRequest("Erro ao criar/alterar o cliente favor contactar a TI");
            }
        }

        [HttpGet("getClienAllResume")]
        public IActionResult GetClienAllResume()
        {
            try
            {
                var result = _clientBusiness.GetClienAllResume();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar os dados do cliente - ", ex);
                return BadRequest("Erro ao buscar os dados do cliente favor contactar a TI");
            }
        }
    }
}
