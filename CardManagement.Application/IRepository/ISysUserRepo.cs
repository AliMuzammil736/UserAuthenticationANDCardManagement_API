using CardManagement.Application.Dtos.SysUser;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.IRepository
{
    public interface ISysUserRepo
    {
        public Task<IdentityResult> CreateUserAsync(SysUser sysUser, string password);
        public Task<SysUser> GetAuthenticateUser(LogInRequest request);

    }
}
