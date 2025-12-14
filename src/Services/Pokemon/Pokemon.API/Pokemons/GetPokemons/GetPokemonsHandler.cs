using BuildingBlocks.CQRS;
using Marten;

namespace Pokemon.API.Pokemons.GetPokemons
{
    public record GetPokemonsQuery() : IQuery<GetPokemonsResult>;
    public record GetPokemonsResult(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsHandler(IDocumentSession session) 
        : IQueryHandler<GetPokemonsQuery, GetPokemonsResult>
    {
        public async Task<GetPokemonsResult> Handle(GetPokemonsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Models.Pokemon>().ToListAsync(cancellationToken);

            return new GetPokemonsResult(products);
        }
    }
}
