using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailWorkshop.Services.Entity
{
    public class Statement
    {
        public Guid Id { get; set; }

        [Required]
        public StatementType Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public Dictionary<Guid, decimal> Products { get; set; }

        [Required]
        public Employee Responsible { get; set; }

        [Required]
        public WorkshopSegment Segment { get; set; }
    }
}