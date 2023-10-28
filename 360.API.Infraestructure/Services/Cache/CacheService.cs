
using StackExchange.Redis;
using _360.API.Domain.Services.Cache;
using _360.API.Domain.Shared;
using _360.API.Infraestructure.Configuration;

namespace _360.API.Infraestructure.Services.Cache
{
   public class CacheService : ICacheService
   {
#pragma warning disable IDE0044 // Agregar modificador de solo lectura
      private IDatabase _db;
#pragma warning restore IDE0044 // Agregar modificador de solo lectura

      public CacheService(AppSettings appSettings)
      {
         var appSet = appSettings;
#pragma warning disable CS8604 // Posible argumento de referencia nulo
         _db = ConnectionMultiplexer.Connect(appSet.RedisURL).GetDatabase();
#pragma warning restore CS8604 // Posible argumento de referencia nulo
      }

      public SZGResponse? GetData(string key)
      {
         SZGResponse? response = null;
         var value = _db.StringGet(key);

         if (!string.IsNullOrEmpty(value))
         {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            response = Newtonsoft.Json.JsonConvert.DeserializeObject<SZGResponse?>(value);
#pragma warning restore CS8604 // Posible argumento de referencia nulo

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (response.Data != null)
            {
#pragma warning disable CS8601 // Posible asignación de referencia nula
               response.Data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json: response?.Data?.ToString() ?? "");
#pragma warning restore CS8601 // Posible asignación de referencia nula
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

         }

         return response;
      }

      public bool SetData(string key, SZGResponse value, DateTimeOffset expirationTime)
      {
         TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
         SZGResponse response = value;

         if (response.Data != null)
#pragma warning disable CS8601 // Posible asignación de referencia nula
            response.Data = response.Data.ToString();
#pragma warning restore CS8601 // Posible asignación de referencia nula

         var isSet = _db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(response), expiryTime);
         if (response.Data != null)
         {
#pragma warning disable CS8601 // Posible asignación de referencia nula
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            response.Data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json: response?.Data?.ToString());
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8601 // Posible asignación de referencia nula
         }

         return isSet;
      }

      public object RemoveData(string key)
      {
         bool _isKeyExist = _db.KeyExists(key);

         if (_isKeyExist == true)
            return _db.KeyDelete(key);

         return false;
      }
   }
}
