namespace RailWorkshop.API.Dto
{
    public class EmployeeUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SegmentId { get; set; }
    }
}