using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Models;

namespace Pokemon.API.Pokemons.CreatePokemon
{
    public record CreatePokemonRequest(string Name, List<Attack> Attacks, string ImageFile);
    public record CreatePokemonResponse(Guid Id);

    internal class CreatePokemonEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/pokemons", async (CreatePokemonRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePokemonCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreatePokemonResponse>();

                return Results.Created($"/pokemons/{response.Id}", response);
            })
                .WithName("CreatePokemon")
                .Produces<CreatePokemonResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Pokemon")
                .WithDescription("Create Pokemon");
        }
    }
}
