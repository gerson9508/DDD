

using _360.API.Domain.Shared;

namespace _360.API.Domain.Interfaces.Query
{
   public interface IQueryRepository
   {
      Task<SZGResponse> GetSICY();
      Task<SZGResponse> GetImage(object request);
      Task<SZGResponse> GetImageTIFF(object request);
   }
}
