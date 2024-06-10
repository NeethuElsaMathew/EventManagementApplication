using FrontEnd_EventManagement.Models;

namespace FrontEnd_EventManagement.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
