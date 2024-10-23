using CardManagement.Application.Dtos.SysUser;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Service.Interface
{
    public interface ISysUserService
    {
        public Task<IdentityResult> CreateUserAsync(RegisterUserRequest registerUser);
        public Task<LogInResponse> GetAuthenticateUser(LogInRequest request);

    }
}
