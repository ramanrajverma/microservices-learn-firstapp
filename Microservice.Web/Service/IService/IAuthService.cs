using Microservice.Web.Models.Dto;
using Microservices.Web.Models;

namespace Microservice.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginResponseDto loginResponseDto);
        Task<ResponseDto> RegisterAsync(LoginResponseDto RegRequestDto);
        Task<ResponseDto> AssignRoleAsync(ResgistrationRequestDto RegRequestDto);
    }
}
