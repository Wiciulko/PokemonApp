using Pokemon.API.Enums;

namespace Pokemon.API.Models
{
    public class Attack
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AttackType AttackType { get; set; }
        public int Damage { get; set; }
    }
}
