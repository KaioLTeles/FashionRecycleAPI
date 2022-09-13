using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionRecycle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpGet("getuser")]
        public IActionResult GetUser(int id)
        {
            
            try
            {                                
                var result = _userBusiness.GetUser(id);
                return Ok(result);                
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar usuario - ", ex);
                return BadRequest("Erro ao criar usuario favor contactar a TI");
            }
        }

        [HttpPost("createUser")]
        public IActionResult CreateUser([FromBody] CreateUserInputModel createUserInputModel)
                {
            try
            {
                _userBusiness.CreateUser(createUserInputModel);
                string msg = "Usuario " + createUserInputModel.Name + " criado com sucesso!";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar usuario - ", ex);
                return BadRequest("Erro ao criar usuario favor contactar a TI");
            }
        }

        [HttpPost("alterUser")]
        public IActionResult AlterUser([FromBody] AlterUserInputModel alterUserInputModel)
        {
            try
            {
                _userBusiness.AlterUser(alterUserInputModel);
                string msg = "Usuario " + alterUserInputModel.name + " criado com sucesso!";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao criar usuario - ", ex);
                return BadRequest("Erro ao criar usuario favor contactar a TI");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginInputModel login)
        {
            try
            {
                return Ok(_userBusiness.Login(login));
            }
            catch(Exception ex)
            {
                Logger.WriteError("Erro ao logar - ", ex);
                return BadRequest("Erro ao logar favor contactar a TI");
            }
        }
        
        [HttpGet("getAllUserByFilter")]
        public IActionResult GetAllUserByFilter(string name, string email)
        {            
            try
            {
                var result = _userBusiness.GetAllUserByFilter(name, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Erro ao buscar dados de usuario - ", ex);
                return BadRequest("Erro ao logar favor contactar a TI");
            }
        }
    }
}
