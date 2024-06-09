namespace FrontEnd_EventManagement.Models
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int EventId { get; set; }
    }
}
