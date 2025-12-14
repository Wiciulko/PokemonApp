using BuildingBlocks.CQRS;
using FluentValidation;
using Marten;
using Pokemon.API.Enums;
using Pokemon.API.Models;

namespace Pokemon.API.Attacks.CreateAttack
{
    public record CreateAttackCommand(string Name, AttackType AttackType, int Damage)
        : ICommand<CreateAttackResult>;
    public record CreateAttackResult(Guid Id);

    public class CreateAttackValidator : AbstractValidator<CreateAttackCommand>
    {
        public CreateAttackValidator()
        {
            RuleFor(attack => attack.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(attack => attack.AttackType).NotEmpty().WithMessage("AttackType is required!");
            RuleFor(attack => attack.Damage).GreaterThan(0).WithMessage("Damage must be greater than 0!");
        }
    }

    internal class CreateAttackHandler(IDocumentSession session)
        : ICommandHandler<CreateAttackCommand, CreateAttackResult>
    {
        public async Task<CreateAttackResult> Handle(CreateAttackCommand command, CancellationToken cancellationToken)
        {
            var attack = new Attack
            {
                Name = command.Name,
                AttackType = command.AttackType,
                Damage = command.Damage,
            };

            session.Store(attack);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateAttackResult(attack.Id);
        }
    }
}
