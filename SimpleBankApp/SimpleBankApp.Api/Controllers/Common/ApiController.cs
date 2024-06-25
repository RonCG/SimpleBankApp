using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Services;
using System.Security.Claims;

namespace SimpleBankApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                return Problem(statusCode: 500);
            }

            var firstError = errors[0];
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }

    }
}
