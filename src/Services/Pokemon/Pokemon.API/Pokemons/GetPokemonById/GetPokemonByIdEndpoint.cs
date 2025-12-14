using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Pokemons.GetPokemonById
{
    public record GetPokemonByIdResponse(Models.Pokemon Pokemon);

    internal class GetPokemonByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/pokemons/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetPokemonByIdQuery(id));
                var response = result.Adapt<GetPokemonByIdResponse>();

                return Results.Ok(response);
            })
                .WithName("GetPokemonById")
                .Produces<GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Pokemon By Id")
                .WithDescription("Get Pokemon By Id");
        }
    }
}
