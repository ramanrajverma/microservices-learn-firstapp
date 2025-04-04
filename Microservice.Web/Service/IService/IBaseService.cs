using Microservice.Web.Models;
using Microservices.Web.Models;

namespace Microservice.Web.Service.IService
{
    public interface IBaseService
    {
      Task<ResponseDto?>SendAsync(RequestDto reqDto);
    }
}
