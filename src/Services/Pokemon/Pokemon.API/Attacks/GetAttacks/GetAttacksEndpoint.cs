using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Models;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Attacks.GetAttacks
{
    public record GetAttacksResponse(IEnumerable<Attack> Attacks);

    internal class GetAttacksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/attacks", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAttacksQuery());
                var response = result.Adapt<GetAttacksResponse>();

                return Results.Ok(response);
            })
                .WithName("GetAttacks")
                .Produces<GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Attacks")
                .WithDescription("Get Attacks");
        }
    }
}
