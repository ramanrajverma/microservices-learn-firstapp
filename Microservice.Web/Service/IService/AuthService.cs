using Microservice.Web.Models.Dto;
using Microservices.Web.Models;

namespace Microservice.Web.Service.IService
{
    public class AuthService : IAuthService
    {
        public Task<ResponseDto> AssignRoleAsync(ResgistrationRequestDto RegRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> LoginAsync(LoginResponseDto loginResponseDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> RegisterAsync(LoginResponseDto RegRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
