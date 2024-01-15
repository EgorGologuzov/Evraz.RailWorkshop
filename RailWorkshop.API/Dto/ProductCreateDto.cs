namespace RailWorkshop.API.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public int SteelId { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}