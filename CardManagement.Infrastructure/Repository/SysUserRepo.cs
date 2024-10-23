using Azure.Core;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Application.IRepository;
using CardManagement.Domain.Model;
using CardManagement.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Infrastructure.Repository
{
    public class SysUserRepo : ISysUserRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<SysUser> _userManager;
        public SysUserRepo(AppDbContext dbContext, UserManager<SysUser> userManager) =>
        (_dbContext, _userManager) = (dbContext ?? throw new ArgumentNullException(nameof(dbContext)),
                                      userManager ?? throw new ArgumentNullException(nameof(userManager)));

        public async Task<IdentityResult> CreateUserAsync(SysUser sysUser, string password)
        {
            var result = await _userManager.CreateAsync(sysUser, password);
            return result;

        }

        public async Task<SysUser> GetAuthenticateUser(LogInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return null; 
            }   
            else { return user; }

        }
    }
}
