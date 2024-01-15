using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class ProductReturnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RailProfile Profile { get; set; }
        public SteelGrade Steel { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}