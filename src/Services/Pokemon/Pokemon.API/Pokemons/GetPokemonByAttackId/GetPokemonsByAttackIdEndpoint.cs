using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Pokemons.GetPokemonByAttackId
{
    public record GetPokemonsByAttackIdResponse(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsByAttackIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/pokemons/attack/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetPokemonsByAttackIdQuery(id));
                var response = result.Adapt<GetPokemonsByAttackIdResponse>();

                return Results.Ok(response);
            })
                .WithName("GetPokemonsByAttackId")
                .Produces<GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Pokemons By Attack Id")
                .WithDescription("Get Pokemons By Attack Id");
        }
    }
}
