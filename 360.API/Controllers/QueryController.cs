namespace _360.API.Controllers
{
   using _360.API.Domain.Interfaces.Query;
   using _360.API.Domain.Shared;
   using Controllers.Base;
   using DTOs.Querys;
   //using Microsoft.AspNetCore.Authorization;
   using Microsoft.AspNetCore.Mvc;
   using System;

   [Authorize]
   [Route("api/v1/query")]
   public class QueryController : BaseController
   {
      #region Properties
      private readonly IQueryRepository _query;
      #endregion

      #region Constructor
      public QueryController(IQueryRepository query) => _query = query;
      #endregion

      #region End Points
      //[Authorize(Roles = "admin")]
      [HttpGet("QueryGet")]
      public async Task<IActionResult> Get([FromQuery] Request request)
      {
         _360Response response = new();
         try { response = await _query.Get(request); }
         catch (Exception ex) { response.Response = ex.Message; }
         return Result(response);
      }

      #endregion
   }
}
