using EventManagement.Data;
using EventManagement.Model;
using EventManagement.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Services
{
    public class RegistrationService
    {
        private readonly ApplicationDbContext _dbContext;

        public RegistrationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Registration>> GetRegistrationsForEventAsync(int eventId)
        {
            return await _dbContext.Registration
                .Where(r => r.EventId == eventId)
                .ToListAsync();
        }
        public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            return await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<Registration?> GetUserRegistrationAsync(int eventId, string emailAddress)
        {
            return await _dbContext.Registration.FirstOrDefaultAsync(r => r.EventId == eventId && r.EmailAddress == emailAddress);
        }

        public async Task<ResponseDTO> AddRegistrationAsync(Registration eventRegistration)
        {
            var response = new ResponseDTO();

            try
            {
                // Check if an event with this eventid is present
                var ifExistingEvent = await GetEventByIdAsync(eventRegistration.EventId);

                if (ifExistingEvent == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Event does not exist, hence cannot be added";
                    return response;
                }

                // Check if the user has already registered for the event
                var ifUserAlreadyRegistered = await GetUserRegistrationAsync(eventRegistration.EventId, eventRegistration.EmailAddress);

                if (ifUserAlreadyRegistered != null)
                {
                    response.IsSuccess = false;
                    response.Message = "This user has already registered for the event, hence cannot be registered again";
                    return response;
                }

                // Add the Registration to the database
                _dbContext.Registration.Add(eventRegistration);
                await _dbContext.SaveChangesAsync();

                response.Message = "Registration added successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding data: {ex.Message}";
            }

            return response;
        }
    }
}
