using FrontEnd_EventManagement.Models;

namespace FrontEnd_EventManagement.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDto, bool withBearer = true);
    }
}
