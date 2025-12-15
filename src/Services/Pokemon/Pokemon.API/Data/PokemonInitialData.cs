using Marten;
using Marten.Schema;
using Pokemon.API.Models;

namespace Pokemon.API.Data
{
    internal class PokemonInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Models.Pokemon>().AnyAsync())
            {
                return;
            }

            session.Store<Attack>(GetInitialAttacks());
            session.Store<Models.Pokemon>(GetInitialPokemons());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Attack> GetInitialAttacks()
        {
            return new List<Attack>
            {
                new Attack
                {
                    Id = new Guid("e2f78c32-f9cc-4d91-a87f-9077600591b3"),
                    Name = "Fire Fang",
                    AttackType = Enums.AttackType.Fire,
                    Damage = 65
                },
                new Attack
                {
                    Id = new Guid("82e1fccf-0adf-4f03-a840-80d1c47493ef"),
                    Name = "Flamethrower",
                    AttackType= Enums.AttackType.Fire,
                    Damage = 90
                },
                new Attack
                {
                    Id = new Guid("680fa2e2-f8ed-4d5b-a746-e86926a6a4e3"),
                    Name = "Lava Plume",
                    AttackType = Enums.AttackType.Fire,
                    Damage = 80
                },
                new Attack
                {
                    Id = new Guid("9ca0c444-dfbf-4c52-a065-3c5b83e66e17"),
                    Name = "Flame Wheel",
                    AttackType = Enums.AttackType.Fire,
                    Damage = 60
                },
                new Attack
                {
                    Id = new Guid("1e72b486-a8cf-4930-a2f7-9a6ba51a7ef0"),
                    Name = "Water Gun",
                    AttackType = Enums.AttackType.Water,
                    Damage = 40
                },
                new Attack
                {
                    Id = new Guid("b41b796c-d7e5-4a69-9405-fbd0b5f4dc65"),
                    Name = "Liquidation",
                    AttackType = Enums.AttackType.Water,
                    Damage = 85
                },
                new Attack
                {
                    Id = new Guid("933093ec-855d-4d48-96a7-3c0936df2808"),
                    Name = "Bubble Beam",
                    AttackType= Enums.AttackType.Water,
                    Damage = 65
                },
                new Attack
                {
                    Id = new Guid("0baa40f3-eef6-4ce3-8475-567168f5056e"),
                    Name = "Hydro Pump",
                    AttackType = Enums.AttackType.Water,
                    Damage = 110
                }
            };
        }

        private static IEnumerable<Models.Pokemon> GetInitialPokemons()
        {
            return new List<Models.Pokemon>
            {
                new Models.Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Charmander",
                    Attacks = GetInitialAttacks().Where(attack => attack.Name == "Fire Fang" || attack.Name == "Flamethrower").ToList(),
                    ImageFile = "charmander.png"
                },
                new Models.Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Flareon",
                    Attacks = GetInitialAttacks().Where(attack => attack.Name == "Lava Plume" || attack.Name == "Flame Wheel").ToList(),
                    ImageFile = "flareon.png"
                },
                new Models.Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Squirtle",
                    Attacks = GetInitialAttacks().Where(attack => attack.Name == "Water Gun" || attack.Name == "Liquidation").ToList(),
                    ImageFile = "squirtle.png"
                },
                new Models.Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Staryu",
                    Attacks = GetInitialAttacks().Where(attack => attack.Name == "Bubble Beam" || attack.Name == "Hydro Pump").ToList(),
                    ImageFile = "staryu.png"
                }
            };
        }
    }
}
