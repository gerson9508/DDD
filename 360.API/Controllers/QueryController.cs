namespace _360.API.Controllers
{
   using _360.API.Domain.Interfaces.Query;
   using _360.API.Domain.Shared;
   using Controllers.Base;
   using DTOs.Querys;
   //using Microsoft.AspNetCore.Authorization;
   using Microsoft.AspNetCore.Mvc;
   using System;

   // [Authorize]
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
      [HttpGet("QueryGetSICY")]
      public async Task<IActionResult> GetSICY()
      {
         SZGResponse response = new();
         try { response = await _query.GetSICY(); }
         catch (Exception ex) { response.Response = ex.Message; }
         return Result(response);
      }

      //[Authorize(Roles = "admin")]
      [HttpGet("QueryGetImage")]
      public async Task<IActionResult> GetImage([FromQuery] ImageRequest request)
      {
         SZGResponse response = new();
         try { response = await _query.GetImage(request); }
         catch (Exception ex) { response.Response = ex.Message; }
         return Result(response);
      }

      [HttpGet("GetImageTIFF")]
      public async Task<IActionResult> GetImageTIFF([FromQuery] ImageTIFFRequest request)
      {
         SZGResponse response = new();
         try { response = await _query.GetImageTIFF(request); }
         catch (Exception ex) { response.Response = ex.Message; }
         return Result(response);
      }
      #endregion
   }
}
