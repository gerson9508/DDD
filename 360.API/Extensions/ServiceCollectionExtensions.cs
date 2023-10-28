namespace _360.API.Extensions
{
   using _360.API.Domain.Interfaces.Base;
   using _360.API.Domain.Interfaces.Query;
   using _360.API.Domain.Services.Cache;
   using _360.API.Infraestructure.Configuration;
   using _360.API.Infraestructure.Repositories.Base;
   using _360.API.Infraestructure.Repositories.Query;
   using _360.API.Infraestructure.Services.Cache;

   public static class ServiceCollectionExtensions
   {
      public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration) =>
         services.AddTransient<IBaseRepository>(s => new BaseRepository(configuration.GetSection("AppSettings").Get<AppSettings>()));
      public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) =>
        services.AddTransient<ICacheService>(s => new CacheService(configuration.GetSection("AppSettings").Get<AppSettings>()));


      public static IServiceCollection AddBusinessServices(this IServiceCollection services) =>
         services.AddTransient<IQueryRepository, QueryRepository>();
   }
}
