using Carter;
using Mapster;
using MediatR;

namespace Pokemon.API.Pokemons.GetPokemons
{
    public record GetPokemonsResponse(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/pokemons", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPokemonsQuery());
                var response = result.Adapt<GetPokemonsResponse>();

                return Results.Ok(response);
            })
                .WithName("GetPokemons")
                .Produces< GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Pokemons")
                .WithDescription("Get Pokemons");
        }
    }
}
