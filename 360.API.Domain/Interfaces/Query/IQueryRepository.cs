

using _360.API.Domain.Shared;

namespace _360.API.Domain.Interfaces.Query
{
   public interface IQueryRepository
   {
      Task<_360Response> Get(object request);
   }
}
