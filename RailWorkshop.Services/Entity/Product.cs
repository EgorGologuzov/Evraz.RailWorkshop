using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailWorkshop.Services.Entity
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ProfileId { get; set; }

        public RailProfile Profile { get; set; }

        [Required]
        public int SteelId { get; set; }

        public SteelGrade Steel { get; set; }

        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> Properties { get; set; }
    }
}