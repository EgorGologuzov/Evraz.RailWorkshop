using RailWorkshop.Services.Entity;

namespace RailWorkshop.API.Dto
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public int SteelId { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}