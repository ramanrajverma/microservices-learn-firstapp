using Microservices.AuthAPI.Models.Dto;

namespace Microservices.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(ResgistrationRequestDto resgistrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string rolename);
    }
}
