using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _360.IdentityServer.Infraestructure.Repositories.Base
{
   public class AspNetIdentityDbContext : IdentityDbContext
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options) : base(options) { }        
    }
}
