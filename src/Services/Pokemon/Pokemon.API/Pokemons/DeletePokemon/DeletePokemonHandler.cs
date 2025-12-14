using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;

namespace Pokemon.API.Pokemons.DeletePokemon
{
    public record DeletePokemonCommand(Guid Id) : ICommand<DeletePokemonResult>;
    public record DeletePokemonResult(bool Success);

    public class DeletePokemonValidator : AbstractValidator<DeletePokemonCommand>
    {
        public DeletePokemonValidator()
        {
            RuleFor(pokemon => pokemon.Id).NotEmpty().WithMessage("Id is required!");
        }
    }

    internal class DeletePokemonHandler(IDocumentSession session)
        : ICommandHandler<DeletePokemonCommand, DeletePokemonResult>
    {
        public async Task<DeletePokemonResult> Handle(DeletePokemonCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Models.Pokemon>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeletePokemonResult(true);
        }
    }
}
