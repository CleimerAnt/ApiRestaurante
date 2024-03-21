using ApiRestaurante.Core.Application.Dto.Account;
using ApiRestaurante.Core.Application.Enums;
using ApiRestaurante.Core.Application.Interfaces.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace ApiRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountServices.AuthenticateASYNC(request));
        }

        [HttpPost("registerWaiter")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> RegisterWaiterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["Origin"];

            return Ok(await _accountServices.RegistrerWaiterUserAsync(request, origin));
        }

        [HttpPost("registerAdmin")]
        [Authorize (Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            var origin = Request.Headers["Origin"];

            return Ok(await _accountServices.RegistrerAdminUserAsync(request, origin));
        }
    }
}
