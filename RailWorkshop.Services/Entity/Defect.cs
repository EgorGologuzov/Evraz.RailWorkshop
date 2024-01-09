using System.ComponentModel.DataAnnotations;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.Services.Entity
{
    public class Defect : IHandbookEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}