﻿using FrontEnd_EventManagement.Models;
using FrontEnd_EventManagement.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace FrontEnd_EventManagement.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventManagementAPI _eventManagementAPI;
        public EventController(IEventManagementAPI eventManagementAPI)
        {
            _eventManagementAPI = eventManagementAPI;
        }
        public async Task<IActionResult> EventIndex()
        {
            List<EventDTO>? list = new();

            ResponseDTO? response = await _eventManagementAPI.GetAllEventsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<EventDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> EventRegistration(int eventId)
        {
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> EventRegistration(RegistrationDTO registrationDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _eventManagementAPI.CreateRegistrationForEventsAsync(registrationDTO);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Registration successfull";
                    return RedirectToAction(nameof(EventIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(registrationDTO);
        }

        public async Task<IActionResult> ViewRegistrationAndCreateEvent()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("~/Views/Shared/Unauthorized.cshtml");
            }

            List<EventDTO>? list = new();

            ResponseDTO? response = await _eventManagementAPI.GetAllEventsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<EventDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CreateEvent()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("~/Views/Shared/Unauthorized.cshtml");
            }

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDTO eventDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _eventManagementAPI.CreateEventAsync(eventDTO);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Event created successfully";
                    return RedirectToAction(nameof(ViewRegistrationAndCreateEvent));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(eventDTO);
        }
    }
}
