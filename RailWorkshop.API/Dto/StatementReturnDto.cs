using RailWorkshop.Services.Entity;
using System.ComponentModel.DataAnnotations;

namespace RailWorkshop.API.Dto
{
    public class StatementReturnDto
    {
        public Guid Id { get; set; }
        public StatementType Type { get; set; }
        public DateTime Date { get; set; }
        public EmployeeReturnDto Responsible { get; set; }
        public WorkshopSegment Segment { get; set; }
        public ICollection<ConsignmentReturnDto> Products { get; set; }
    }
}