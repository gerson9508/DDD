

using _360.API.Domain.Shared;

namespace _360.API.Domain.Services.Cache
{
   public interface ICacheService
   {
      /// <summary>
      /// Get Data using key
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="key"></param>
      /// <returns></returns>
      _360Response? GetData(string key);


      /// <summary> 
      /// Set Data with Value and Expiration Time of Key
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="key"></param>
      /// <param name="value"></param>
      /// <param name="expirationTime"></param>
      /// <returns></returns>
      bool SetData(string key, _360Response value, DateTimeOffset expirationTime);

      /// <summary>
      /// Remove Data 
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      object RemoveData(string key);
   }
}
