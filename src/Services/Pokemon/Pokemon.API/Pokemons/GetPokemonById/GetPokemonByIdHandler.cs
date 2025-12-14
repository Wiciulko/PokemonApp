using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
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
                throw new NotFoundException("Pokemon", query.Id);
            }

            return new GetPokemonByIdResult(pokemon);
        }
    }
}
