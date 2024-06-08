using EventManagement.Data;
using EventManagement.Model;
using EventManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _dbContext;

        public EventService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDTO> AddEventAsync(Event eventToCreate)
        {
            var response = new ResponseDTO();
            try
            {
                // Check if the start time and end time are in the future
                if (eventToCreate.StartTime <= DateTime.Now || eventToCreate.EndTime <= DateTime.Now)
                {
                    response.IsSuccess = false;
                    response.Message = "Event starttime or endtime should be in future";
                    return response;
                }

                // Check if the end time is after the start time
                if (eventToCreate.EndTime <= eventToCreate.StartTime)
                {
                    response.IsSuccess = false;
                    response.Message = "Event endtime should be after starttime";
                    return response;
                }

                // Check if an event with the same properties already exists
                var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e =>
                    e.Name == eventToCreate.Name &&
                    e.Description == eventToCreate.Description &&
                    e.Location == eventToCreate.Location &&
                    e.StartTime == eventToCreate.StartTime &&
                    e.EndTime == eventToCreate.EndTime);

                if (existingEvent != null)
                {
                    response.IsSuccess = false;
                    response.Message = "This event is already registered,hence cannot be re-registered";
                    return response;

                }

                _dbContext.Events.Add(eventToCreate);
                await _dbContext.SaveChangesAsync();
                response.Message = "Event added successfully";


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding data: {ex.Message}";
            }
            return response;

        }

        public async Task<ResponseDTO> GetRegistrationsForEventResponseAsync(int eventId)
        {
            var response = new ResponseDTO();
            try
            {
                var eventExists = await _dbContext.Events.AnyAsync(e => e.Id == eventId);

                if (!eventExists)
                {
                    response.IsSuccess = false;
                    response.Message = "Event not found";
                    return response;
                }

                var registrations = await GetRegistrationsForEventAsync(eventId);
                response.Result = registrations;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving data: {ex.Message}";
            }

            return response;
        }

        public async Task<List<Registration>> GetRegistrationsForEventAsync(int eventId)
        {
            return await _dbContext.Registration
                .Where(r => r.EventId == eventId)
                .ToListAsync();
        }
    }
}
