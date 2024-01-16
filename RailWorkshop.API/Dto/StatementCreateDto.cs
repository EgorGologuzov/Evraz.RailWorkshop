using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class StatementCreateDto
    {
        public StatementType Type { get; set; }
        public DateTime? Date { get; set; }
        public Guid? ResponsibleId { get; set; }
        public int? SegmentId { get; set; }
        public ICollection<ConsignmentCreateDto> Products { get; set; }
    }
}