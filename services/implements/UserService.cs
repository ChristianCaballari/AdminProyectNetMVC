using System.Security.Claims;

namespace AdminProyectos.services.implements
{
    public class UserService : IUsersService
    {
        private readonly HttpContext _httpContext;

        public UserService(IHttpContextAccessor httpContextAccessor) {
            this._httpContext = httpContextAccessor.HttpContext;
        }
        public int GetUserId()
        {
            var d = _httpContext.User.Claims;

            if (_httpContext.User.Identity.IsAuthenticated) {
                var idClaim = _httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                return int.Parse(idClaim.Value);
            }
            else 
            { 
                throw new ApplicationException("El usuario no está autenticado");
            }
        }
    }
}
