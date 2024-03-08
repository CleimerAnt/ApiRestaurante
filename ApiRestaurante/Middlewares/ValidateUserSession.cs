using ApiRestaurante.Core.Application.Dto.Account;
using ApiRestaurante.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace ApiRestaurante.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public bool HasUser()
        {
            AutenticationResponse usuarioViewModel = _contextAccessor.HttpContext.Session.get<AutenticationResponse>("User");
            if (usuarioViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
