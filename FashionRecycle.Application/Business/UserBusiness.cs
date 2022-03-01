using AutoMapper;
using FashionRecycle.API.Core.Entity;
using FashionRecycle.API.Core.Enums;
using FashionRecycle.API.Core.InputModel;
using FashionRecycle.API.Core.Interface;
using FashionRecycle.API.Core.ViewModel;
using FashionRecycle.Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FashionRecycle.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserBusiness(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void CreateUser(CreateUserInputModel createUserInputModel)
        {
            var userEntity = _mapper.Map<UserEntity>(createUserInputModel);

            userEntity.Password = Cryptography.ComputeSha256Hash(userEntity.Password);

            _userRepository.CreateUser(userEntity);                      
        }

        public LoginViewModel Login(LoginInputModel loginInputModel)
        {            
            var encryptedPassword = Cryptography.ComputeSha256Hash(loginInputModel.Password);

            var user = _userRepository.GetUserByUserName(loginInputModel.Username);                                            

            if (user != null)
            {
               if(user.Password == encryptedPassword)
                {
                    return new LoginViewModel
                    {
                        Email = user.Email,
                        Token = GenerateJwtToken(user.Email, user.RoleId.ToString()),
                        LoginStatus = (int)LoginStatusEnum.LoginSucess,
                        Message = "Login realizado com sucesso!"
                    };
                }
                else
                {
                    return new LoginViewModel
                    {
                        Email = String.Empty,
                        Token = String.Empty,
                        LoginStatus = (int)LoginStatusEnum.LoginFail,
                        Message = "Usuario ou senha incorreto!"
                    };
                }
            }
            else
            {
                return new LoginViewModel
                {
                    Email = String.Empty,
                    Token = String.Empty,
                    LoginStatus = (int)LoginStatusEnum.LoginFail,
                    Message = "Usuario ou senha incorreto!"
                };
            }           
        }
        private string GenerateJwtToken(string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials, claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
