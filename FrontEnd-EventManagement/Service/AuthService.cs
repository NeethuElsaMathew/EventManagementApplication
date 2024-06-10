using FrontEnd_EventManagement.Models;
using FrontEnd_EventManagement.Service.IService;
using FrontEnd_EventManagement.Utility;

namespace FrontEnd_EventManagement.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.ApiTypes.POST,
                Data = loginRequestDto,
                Url = ApiType.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }
    }
}
