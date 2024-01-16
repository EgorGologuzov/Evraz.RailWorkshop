using System.ComponentModel.DataAnnotations;

namespace RailWorkshop.Services.Entity
{
    public class Statement
    {
        public Guid Id { get; set; }

        [Required]
        public StatementType Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<Consignment> Products { get; set; }

        [Required]
        public Guid ResponsibleId { get; set; }

        public Employee Responsible { get; set; }

        [Required]
        public int SegmentId { get; set; }

        public WorkshopSegment Segment { get; set; }
    }
}