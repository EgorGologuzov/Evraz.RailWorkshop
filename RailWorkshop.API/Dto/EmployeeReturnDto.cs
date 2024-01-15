using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class EmployeeReturnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public WorkshopSegment Segment { get; set; }
    }
}