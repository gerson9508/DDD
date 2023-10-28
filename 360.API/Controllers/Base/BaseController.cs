namespace _360.API.Controllers.Base
{
   using _360.API.Domain.Shared;
   using _360.API.Domain.Shared.Constants;
   using Microsoft.AspNetCore.Mvc;

   [ApiController]
   public class BaseController : ControllerBase
   {
      #region Methods Protected
      protected ActionResult Result(_360Response response) =>
         response.Status switch
         {
            StatusCodes.Ok => Ok(response),
            StatusCodes.BadRequest => BadRequest(response),
            _ => StatusCode(StatusCodes.InsernalServerError, response),
         };
      #endregion
   }
}
