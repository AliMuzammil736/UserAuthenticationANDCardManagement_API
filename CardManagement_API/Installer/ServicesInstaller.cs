using CardManagement.Application.IRepository;
using CardManagement.Application.Service.Definition;
using CardManagement.Application.Service.Interface;
using CardManagement.Application.Utils;
using CardManagement.Infrastructure.Repository;

namespace CardManagement_API.Installer
{
    public class ServicesInstaller
    {
        public static void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISysUserService, SysUserService>();
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<ISysUserRepo, SysUserRepo>();
            builder.Services.AddScoped<ICardRepo, CardRepo>();
            builder.Services.AddScoped<IAPIClientRepo, APIClientRepo>();
            builder.Services.AddScoped<Helper>();
        }
    }
}
