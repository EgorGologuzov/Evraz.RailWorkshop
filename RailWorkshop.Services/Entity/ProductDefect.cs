using System.ComponentModel.DataAnnotations;

namespace RailWorkshop.Services.Entity
{
    public class ProductDefect
    {
        public int Id { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public Defect Defect { get; set; }

        public decimal Quantity { get; set; }
        public decimal Size { get; set; }
    }
}