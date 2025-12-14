using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Pokemons.GetPokemons;

namespace Pokemon.API.Pokemons.DeletePokemon
{
    public record DeletePokemonResponse(bool Success);

    public class DeletePokemonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/pokemons/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeletePokemonCommand(id));
                var response = result.Adapt<DeletePokemonResponse>();

                return Results.Ok(response);
            })
                .WithName("DeletePokemon")
                .Produces<GetPokemonsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Pokemon")
                .WithDescription("Delete Pokemon");
        }
    }
}
