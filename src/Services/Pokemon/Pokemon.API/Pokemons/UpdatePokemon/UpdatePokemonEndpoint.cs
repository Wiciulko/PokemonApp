using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Models;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Pokemons.UpdatePokemon
{
    public record UpdatePokemonRequest(Guid Id, string Name, List<Attack> Attacks, string ImageFile);
    public record UpdatePokemonResponse(bool Success);

    internal class UpdatePokemonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/pokemons", async (UpdatePokemonRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdatePokemonCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdatePokemonResponse>();

                return Results.Ok(response);
            })
                .WithName("UpdatePokemon")
                .Produces<GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Pokemon")
                .WithDescription("Update Pokemon");
        }
    }
}
