using FrontEnd_EventManagement.Models;

namespace FrontEnd_EventManagement.Service.IService
{
    public interface IEventManagementAPI
    {
        Task<ResponseDTO?> GetAllEventsAsync();
        Task<ResponseDTO?> CreateRegistrationForEventsAsync(RegistrationDTO registrationDTO);
        Task<ResponseDTO?> GetAllRegistrationForEventAsync(int eventId);
        Task<ResponseDTO?> CreateEventAsync(EventDTO eventDto);
    }
}
