using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        public string Role { get; set; }= "Event_Creator";
    }
}
