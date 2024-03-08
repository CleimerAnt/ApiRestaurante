using ApiRestaurante.Core.Application.Dto.Account;

namespace ApiRestaurante.Core.Application.Interfaces.Account
{
    public interface IAccountServices
    {
        Task<AutenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<RegisterResponse> RegistrerAdminUserAsync(RegisterRequest request, string origin);
        Task SingOutAsync();
        Task<RegisterResponse> RegistrerWaiterUserAsync(RegisterRequest request, string origin);
    }
}