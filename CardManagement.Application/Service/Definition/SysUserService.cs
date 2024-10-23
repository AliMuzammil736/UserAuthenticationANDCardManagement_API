using CardManagement.Application.Dtos.SysUser;
using CardManagement.Application.IRepository;
using CardManagement.Application.Service.Interface;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Service.Definition
{
    public class SysUserService :ISysUserService
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly IConfiguration _configuration;

        public SysUserService(ISysUserRepo sysUserRepo , IConfiguration configuration) =>
         (_sysUserRepo, _configuration) = (sysUserRepo ?? throw new ArgumentNullException(nameof(sysUserRepo)),
                                            configuration ?? throw new ArgumentNullException(nameof(configuration)));

        public async Task<IdentityResult> CreateUserAsync(RegisterUserRequest registerUser)
        {
            return await _sysUserRepo.CreateUserAsync(new SysUser { UserName = registerUser.Email, Email = registerUser.Email }, registerUser.Password);
        }

        public async Task<LogInResponse> GetAuthenticateUser(LogInRequest request)
        {
            var user = await _sysUserRepo.GetAuthenticateUser(request);

            if(user != null)
            {
                return new LogInResponse()
                {
                    ID= user.Id,
                    Username = user.UserName,
                    Token = GenerateJwtToken(user)
                };
            }
            else
            {
                return null;
            }
       
        }

        private string GenerateJwtToken(SysUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:ExpiryInMins"])),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"], 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

