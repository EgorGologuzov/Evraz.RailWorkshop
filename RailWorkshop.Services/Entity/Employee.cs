using System.ComponentModel.DataAnnotations;
using RailWorkshop.Services.Utils;

namespace RailWorkshop.Services.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int SegmentId { get; set; }

        public WorkshopSegment Segment { get; set; }
    }
}