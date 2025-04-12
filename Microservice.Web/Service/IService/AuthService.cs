using Microservice.Web.Models;
using Microservice.Web.Models.Dto;
using Microservice.Web.Utility;
using Microservices.Web.Models;

namespace Microservice.Web.Service.IService
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
                _baseService = baseService;
        }
        public async Task<ResponseDto> AssignRoleAsync(ResgistrationRequestDto RegRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = RegRequestDto,
                Url = StaticDetails.AuthApiBase + "/api/Auth" + "/AssignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginResponseDto loginResponseDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginResponseDto,
                Url = StaticDetails.AuthApiBase + "/api/Auth" + "/login"
            });
        }

        public async Task<ResponseDto> RegisterAsync(LoginResponseDto RegRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = RegRequestDto,
                Url = StaticDetails.AuthApiBase + "/api/Auth" + "/register"
            }); throw new NotImplementedException();
        }
    }
}
