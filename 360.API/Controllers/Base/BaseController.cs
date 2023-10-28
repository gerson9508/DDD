namespace _360.API.Controllers.Base
{
   using Microsoft.AspNetCore.Mvc;
   using _360.API.Domain.Shared.Constants;
   using _360.API.Domain.Shared;

   [ApiController]
   public class BaseController : ControllerBase
   {
      #region Methods Protected
      protected ActionResult Result(SZGResponse response) =>
         response.Status switch
         {
            StatusCodes.Ok => Ok(response),
            StatusCodes.BadRequest => BadRequest(response),
            _ => StatusCode(StatusCodes.InsernalServerError, response),
         };
      #endregion
   }
}
