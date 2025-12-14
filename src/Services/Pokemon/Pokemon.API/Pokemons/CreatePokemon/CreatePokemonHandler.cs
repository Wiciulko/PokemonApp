using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;
using Pokemon.API.Models;

namespace Pokemon.API.Pokemons.CreatePokemon
{
    public record CreatePokemonCommand(string Name, List<Attack> Attacks, string ImageFile)
        : ICommand<CreatePokemonResult>;
    public record CreatePokemonResult(Guid Id);

    public class CreatePokemonValidator : AbstractValidator<CreatePokemonCommand>
    {
        public CreatePokemonValidator()
        {
            RuleFor(pokemon => pokemon.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(pokemon => pokemon.Attacks).NotEmpty().WithMessage("At least one Attack is required!");
            RuleFor(pokemon => pokemon.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        }
    }

    internal class CreatePokemonHandler(IDocumentSession session) 
        : ICommandHandler<CreatePokemonCommand, CreatePokemonResult>
    {
        public async Task<CreatePokemonResult> Handle(CreatePokemonCommand command, CancellationToken cancellationToken)
        {
            var pokemon = new Models.Pokemon
            {
                Name = command.Name,
                Attacks = command.Attacks,
                ImageFile = command.ImageFile
            };

            session.Store(pokemon);
            await session.SaveChangesAsync(cancellationToken);

            return new CreatePokemonResult(pokemon.Id);
        }
    }
}
