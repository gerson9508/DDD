namespace _360.API.Domain.Interfaces.Base
{
   public interface IBaseRepository
   {
      Task<T> GetMapperData<T>(string storedProcedureName, object objParameters);
   }
}
