namespace _360.API.Infraestructure.Repositories.Query
{
   using _360.API.Domain.Interfaces.Base;
   using _360.API.Domain.Interfaces.Query;
   using _360.API.Domain.Services.Cache;
   using _360.API.Domain.Shared;
   using _360.API.Infraestructure.StoreProcedures;

   public class QueryRepository : IQueryRepository
   {
      #region Properties
      private readonly IBaseRepository _repository;
      private readonly ICacheService _cache;
      private readonly DateTime _timeHoursCache = DateTime.Now.AddHours(8);
      #endregion

      #region Constructor
      public QueryRepository(IBaseRepository repository, ICacheService cache)
      {
         _repository = repository;
         _cache = cache;
      }
      #endregion

      #region Methods
      public async Task<SZGResponse> GetImage(object request)
      {
         var key = $"Image{Newtonsoft.Json.JsonConvert.SerializeObject(request)}";
         var response = _cache.GetData(key);

         if (response == null)
         {
            response = await _repository.GetMapperData<SZGResponse>(SpName.GetImage, request);
            _cache.SetData(key, response, _timeHoursCache);
         }

         return response;
      }

      public async Task<SZGResponse> GetImageTIFF(object request)
      {
         var key = $"ImageTIFF{Newtonsoft.Json.JsonConvert.SerializeObject(request)}";
         var response = _cache.GetData(key);

         if (response == null)
         {
            response = await _repository.GetMapperData<SZGResponse>(SpName.GetImageTIFF, request);
            _cache.SetData(key, response, _timeHoursCache);
         }

         return response;
      }

      public async Task<SZGResponse> GetSICY()
      {
         var key = "SICY3";
         var response = _cache.GetData(key);

         if (response == null)
         {
            response = await _repository.GetMapperData<SZGResponse>(SpName.QueryGetSICY, new { });
            _cache.SetData(key, response, _timeHoursCache);
         }

         return response;
      }

      #endregion
   }
}
