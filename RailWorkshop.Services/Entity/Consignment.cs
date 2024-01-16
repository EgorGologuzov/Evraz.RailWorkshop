using System.ComponentModel.DataAnnotations;

namespace RailWorkshop.Services.Entity
{
    public class Consignment
    {
        public Guid Id { get; set; }

        [Required]
        public Guid StatementId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public string Stamp { get; set; }

        public decimal Quantity { get; set; }

        public ICollection<ConsignmentDefect> Defects { get; set; }
    }
}