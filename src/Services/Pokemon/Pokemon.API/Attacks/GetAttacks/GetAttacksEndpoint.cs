using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Models;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Attacks.GetAttacks
{
    public record GetAttacksRequest(int? PageNumber = 1, int? PageSize = 20);
    public record GetAttacksResponse(IEnumerable<Attack> Attacks);

    internal class GetAttacksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/attacks", async ([AsParameters]GetAttacksRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetAttacksQuery>();
                var result = await sender.Send(query);
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
