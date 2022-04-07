using System.Security.Claims;

namespace toDo_API.Services.UserService 
{
    public class UserService: IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetMyID()
        {
            var result = string.Empty;
            if (_httpContextAccessor != null) {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return result;
        }
    }
}