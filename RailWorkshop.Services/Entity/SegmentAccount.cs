using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailWorkshop.Services.Entity
{
    public class SegmentAccount
    {
        [Key]
        public int SegmentId { get; set; }

        public WorkshopSegment Segment { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public Dictionary<Guid, decimal> Products { get; set; }
    }
}