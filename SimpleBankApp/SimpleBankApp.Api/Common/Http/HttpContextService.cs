using System.Security.Claims;

namespace SimpleBankApp.Api.Common.Http
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetUserId()
        {
            var subValue = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool validGuid = Guid.TryParse(subValue, out Guid userGuid);
            if (validGuid)
            {
                return userGuid;
            }

            return Guid.Empty;
        }
    }

    public interface IHttpContextService
    {
        public Guid GetUserId();
    }
}
