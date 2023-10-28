using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _360.IdentityServer.Infraestructure.Repositories.Base;

namespace _360.IdentityServer.Extensions
{
   public static class ServiceCollectionExtensions
   {
      public static IServiceCollection Identity(this IServiceCollection services, 
         IConfiguration configuration, 
         IWebHostEnvironment environment)
      {
         var assembly = typeof(Program).Assembly.GetName().Name;
         var defaultConnString = configuration.GetConnectionString("SQLServerConnection");

         services.AddDbContext<AspNetIdentityDbContext>(options => options.UseSqlServer(defaultConnString, b => b.MigrationsAssembly(assembly)));
         services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityDbContext>();

         var identity = services.AddIdentityServer().AddAspNetIdentity<IdentityUser>()
            .AddConfigurationStore(options =>
            {
               options.ConfigureDbContext = b => b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
            })
            .AddOperationalStore(options =>
            {
               options.ConfigureDbContext = b => b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
            });

         //not recommended for production - you need to store your key material somewhere secure        
         if (environment.IsDevelopment())
            identity.AddDeveloperSigningCredential();
         else
         {
            string path = Path.Combine(environment.ContentRootPath, configuration.GetConnectionString("CertName")),
               pwd = configuration.GetConnectionString("CertPwd");
            //Build the file path.                        
            identity.AddSigningCredential(Config.GetCert(path, pwd));
         }

         return services;
      }
   }
}
