namespace Pokemon.API.Models
{
    public class Pokemon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Attack> Attacks { get; set; } = new();
        public string ImageFile { get; set; }
    }
}
