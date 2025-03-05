using apiStock.BLL.Services;
using apiStock.DAL.Context;
using apiStock.DAL.Repository;
using apiStock.DAL.Repository.Contract;
using apiStock.Utility;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

namespace apiStock.IOC
{
    public static class Dependency
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(configuration.GetConnectionString("sqlConection"));
            });

            services.AddTransient(typeof(IGenerycRepository<>), typeof(GenerycRepository<>));
            
            //add automapper class
            services.AddAutoMapper(typeof(AutoMappeeProfile));

            services.AddScoped<IRoleServices, RoleService>();

            services.AddScoped<_encriptService>();

        }


        
    }
}
