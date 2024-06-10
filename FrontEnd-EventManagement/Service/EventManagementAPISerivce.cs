using FrontEnd_EventManagement.Models;
using FrontEnd_EventManagement.Service.IService;
using FrontEnd_EventManagement.Utility;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace FrontEnd_EventManagement.Service
{
    public class EventManagementAPISerivce : IEventManagementAPI
    {
        private readonly IBaseService _baseService;
        public EventManagementAPISerivce(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateEventAsync(EventDTO eventDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.ApiTypes.POST,
                Data = eventDto,
                Url = ApiType.EventManagementAPI + "/api/Event"
            });
        }

        public async Task<ResponseDTO?> CreateRegistrationForEventsAsync(RegistrationDTO registrationDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.ApiTypes.POST,
                Data = registrationDTO,
                Url = ApiType.EventManagementAPI + "/api/Registration"
            });
        }

        public async Task<ResponseDTO?> GetAllEventsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.ApiTypes.GET,
                Url = ApiType.EventManagementAPI + "/api/Event/all/events"
            });
        }

        public async Task<ResponseDTO?> GetAllRegistrationForEventAsync(int eventId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.ApiTypes.GET,
                Url = ApiType.EventManagementAPI + "/api/Event/registrations/"+eventId
            });
        }
    }
}
