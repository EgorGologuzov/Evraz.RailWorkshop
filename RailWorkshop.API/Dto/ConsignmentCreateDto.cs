namespace RailWorkshop.API.Dto
{
    public class ConsignmentCreateDto
    {
        public string Stamp { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public ICollection<ConsignmentDefectCreateDto>? Defects { get; set; }
    }
}