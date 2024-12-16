using Championchip.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Championship.Presentation.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        public ActionResult ProcessError(ApiBaseResponse baseResponse)
        {

            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound(Results.Problem
                (
                   detail: ((ApiNotFoundResponse)baseResponse).Message,
                   statusCode: StatusCodes.Status404NotFound,
                   title: ((ApiNotFoundResponse)baseResponse).Title,
                   instance: HttpContext.Request.Path
                )),
                TournamentIsFullResponse => NotFound(Results.Problem
                (
                   detail: $"Tournament with id: {((TournamentIsFullResponse)baseResponse).Id} is full",
                   statusCode: StatusCodes.Status404NotFound,
                   title: "Tournament full",
                   instance: HttpContext.Request.Path
                )),
                _ => throw new NotImplementedException()
            };

        }
    }
}
