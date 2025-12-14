using BuildingBlocks.CQRS;
using Marten;

namespace Pokemon.API.Pokemons.GetPokemonById
{
    public record GetPokemonByIdQuery(Guid Id) : IQuery<GetPokemonByIdResult>;
    public record GetPokemonByIdResult(Models.Pokemon Pokemon);

    internal class GetPokemonByIdHandler(IDocumentSession session)
        : IQueryHandler<GetPokemonByIdQuery, GetPokemonByIdResult>
    {
        public async Task<GetPokemonByIdResult> Handle(GetPokemonByIdQuery query, CancellationToken cancellationToken)
        {
            var pokemon = await session.LoadAsync<Models.Pokemon>(query.Id, cancellationToken);

            if (pokemon is null)
            {
                throw new Exception($"Pokemon with id:{query.Id} not found");
            }

            return new GetPokemonByIdResult(pokemon);
        }
    }
}
