using System.ComponentModel.DataAnnotations;
using RailWorkshop.Services.Interfaces;

namespace RailWorkshop.Services.Entity
{
    public class SteelGrade : IHandbookEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}