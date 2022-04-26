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

        [HttpGetAttribute("getuser/{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("createUser")]
        public IActionResult CreateUser(CreateUserInputModel createUserInputModel)
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
    }
}
