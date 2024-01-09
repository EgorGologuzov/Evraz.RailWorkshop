using System.ComponentModel.DataAnnotations;
using RailWorkshop.Services.Utils;

namespace RailWorkshop.Services.Entity
{
    public class Employee
    {
        private string _password;
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password
        {
            get => _password;
            set => _password = value.ToSha256Hash();
        }

        [Required]
        public WorkshopSegment Segment { get; set; }
    }
}