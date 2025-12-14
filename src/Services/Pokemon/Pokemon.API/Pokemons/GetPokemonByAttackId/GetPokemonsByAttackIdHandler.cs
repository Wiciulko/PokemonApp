using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;

namespace Pokemon.API.Pokemons.GetPokemonByAttackId
{
    public record GetPokemonsByAttackIdQuery(Guid Id) : IQuery<GetPokemonsByAttackIdResult>;
    public record GetPokemonsByAttackIdResult(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsByAttackIdHandler(IDocumentSession session)
        : IQueryHandler<GetPokemonsByAttackIdQuery, GetPokemonsByAttackIdResult>
    {
        public async Task<GetPokemonsByAttackIdResult> Handle(GetPokemonsByAttackIdQuery query, CancellationToken cancellationToken)
        {
            var pokemons = await session.Query<Models.Pokemon>()
                .Where(pokemon => pokemon.Attacks.Any(attack => attack.Id == query.Id))
                .ToListAsync(cancellationToken);

            return new GetPokemonsByAttackIdResult(pokemons);
        }
    }
}
