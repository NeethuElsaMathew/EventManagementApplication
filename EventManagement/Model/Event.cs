using System.ComponentModel.DataAnnotations;

namespace EventManagement.Model
{
    public class Event
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required] 
        public DateTime StartTime { get; set; }
        [Required] 
        public DateTime EndTime { get; set; }
        
    }
}
