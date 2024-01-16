using System.ComponentModel.DataAnnotations;

namespace RailWorkshop.Services.Entity
{
    public class ConsignmentDefect
    {
        [Required]
        public Guid ConsignmentId { get; set; }

        [Required]
        public int DefectId { get; set; }

        public Defect Defect { get; set; }

        public decimal Size { get; set; }
    }
}