using BuildingBlocks.CQRS;
using Marten;
using Marten.Pagination;

namespace Pokemon.API.Pokemons.GetPokemons
{
    public record GetPokemonsQuery(int? PageNumber = 1, int? PageSize = 20) : IQuery<GetPokemonsResult>;
    public record GetPokemonsResult(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsHandler(IDocumentSession session) 
        : IQueryHandler<GetPokemonsQuery, GetPokemonsResult>
    {
        public async Task<GetPokemonsResult> Handle(GetPokemonsQuery query, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Models.Pokemon>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 20, cancellationToken);

            return new GetPokemonsResult(products);
        }
    }
}
