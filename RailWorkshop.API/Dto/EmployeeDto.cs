namespace RailWorkshop.API.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int SegmentId { get; set; }
    }
}