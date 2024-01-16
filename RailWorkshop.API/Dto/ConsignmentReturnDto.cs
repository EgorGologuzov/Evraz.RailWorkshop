using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class ConsignmentReturnDto
    {
        public string Stamp { get; set; }
        public ProductReturnDto Product { get; set; }
        public decimal Quantity { get; set; }
        public ICollection<ConsignmentDefectReturnDto> Defects { get; set; }
    }
}