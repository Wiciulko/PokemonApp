using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using Marten;
using Pokemon.API.Models;

namespace Pokemon.API.Pokemons.UpdatePokemon
{
    public record UpdatePokemonCommand(Guid Id, string Name, List<Attack> Attacks, string ImageFile)
        : ICommand<UpdatePokemonResult>;
    public record UpdatePokemonResult(bool Success);

    public class UpdatePokemonValidator : AbstractValidator<UpdatePokemonCommand>
    {
        public UpdatePokemonValidator()
        {
            RuleFor(pokemon => pokemon.Id).NotEmpty().WithMessage("Id is required!");
            RuleFor(pokemon => pokemon.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(pokemon => pokemon.Attacks).NotEmpty().WithMessage("At least one Attack is required!");
            RuleFor(pokemon => pokemon.ImageFile).NotEmpty().WithMessage("ImageFile is required!");
        }
    }

    internal class UpdatePokemonHandler(IDocumentSession session)
        : ICommandHandler<UpdatePokemonCommand, UpdatePokemonResult>
    {
        public async Task<UpdatePokemonResult> Handle(UpdatePokemonCommand command, CancellationToken cancellationToken)
        {
            var pokemon = await session.LoadAsync<Models.Pokemon>(command.Id, cancellationToken);

            if (pokemon is null)
            {
                throw new NotFoundException("Pokemon", command.Id);
            }

            pokemon.Name = command.Name;
            pokemon.Attacks = command.Attacks;
            pokemon.ImageFile = command.ImageFile;

            session.Update(pokemon);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdatePokemonResult(true);
        }
    }
}
