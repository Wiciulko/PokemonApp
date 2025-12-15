using Carter;
using Mapster;
using MediatR;

namespace Pokemon.API.Pokemons.GetPokemons
{
    public record GetPokemonsRequest(int? PageNumber = 1, int? PageSize = 20);
    public record GetPokemonsResponse(IEnumerable<Models.Pokemon> Pokemons);

    internal class GetPokemonsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/pokemons", async ([AsParameters]GetPokemonsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetPokemonsQuery>();
                var result = await sender.Send(query);
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
