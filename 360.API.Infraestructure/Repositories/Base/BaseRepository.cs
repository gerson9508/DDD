namespace _360.API.Infraestructure.Repositories.Base
{
   using Dapper;
   using Microsoft.Data.SqlClient;
   using System.Data;
   using System.Data.Common;
   using System.Text.Json;
   using _360.API.Domain.Interfaces.Base;
   using _360.API.Infraestructure.Configuration;

   public class BaseRepository : IBaseRepository
   {
      #region Properties
      private readonly Func<DbConnection> ConnectionFactory;
      private int CommandTimeOut { get; set; }
      #endregion

      #region Contructor
      public BaseRepository(AppSettings appSettings)
      {
         ConnectionFactory = () => new SqlConnection(appSettings.ConnectionString);
         CommandTimeOut = Convert.ToInt32(appSettings.CommandTimeOut);
      }
      #endregion

      #region Methods
      public async Task<T> GetMapperData<T>(string spName, object objParameters)
      {
         try
         {
            string result;
            using (var connection = ConnectionFactory())
            {
               connection.Open();
               var transaction = connection.BeginTransaction();
               result = await connection.QuerySingleOrDefaultAsync<string>(
               sql: spName,
               param: objParameters,
               transaction: transaction,
               commandTimeout: Convert.ToInt32(CommandTimeOut),
               commandType: CommandType.StoredProcedure);
               transaction.Commit();
            }
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return JsonSerializer.Deserialize<T>(result);
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
         }
         catch (Exception) { throw; }
      }
      #endregion
   }
}
