using FrontEnd_EventManagement.Models;
using FrontEnd_EventManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd_EventManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IEventManagementAPI _eventManagementAPI;
        public RegistrationController(IEventManagementAPI eventManagementAPI)
        {
            _eventManagementAPI = eventManagementAPI;
        }
        public async Task<IActionResult> RegistrationList(int eventId)
        {
            List<RegistrationDTO>? list = new();

            ResponseDTO? response = await _eventManagementAPI.GetAllRegistrationForEventAsync(eventId);

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<RegistrationDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);

        }
    }
}
