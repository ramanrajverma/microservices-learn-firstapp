using Microservice.Web.Models;
using Microservice.Web.Service.IService;
using Microservices.Web.Models;
using System.Text.Json;
using System.Text;
using static Microservice.Web.Utility.StaticDetails;
using System.Linq.Expressions;
using System.Net;
using Newtonsoft.Json;


namespace Microservice.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        public BaseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto reqDto)
        {
            try 
            {
                HttpClient client = _clientFactory.CreateClient("MicroservicesApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                //TODO: Add JWT Token

                message.RequestUri = new Uri(reqDto.Url);
                if (reqDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(reqDto.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                HttpResponseMessage? apiResponse = null;
                switch (reqDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Not Found",
                        };
                    case HttpStatusCode.Unauthorized:
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Unauthorized Access!",
                        };
                    case HttpStatusCode.InternalServerError:
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Internal Server Error!",
                        };
                    case HttpStatusCode.Forbidden:
                        return new()
                        {
                            IsSuccess = false,
                            Message = "Forbidden Access!",
                        };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        //var apiResponseDto = JsonSerializer.Deserializeo<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex) 
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false,
                };
                return dto;
            }
 
        }
    }
}
