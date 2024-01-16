using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class ConsignmentDefectReturnDto
    {
        public Defect Defect { get; set; }
        public decimal Size { get; set; }
    }
}