using BuildingBlocks.CQRS;
using Marten;
using Pokemon.API.Models;

namespace Pokemon.API.Attacks.GetAttacks
{
    public record GetAttacksQuery() : IQuery<GetAttacksResult>;
    public record GetAttacksResult(IEnumerable<Attack> Attacks);

    internal class GetAttacksHandler(IDocumentSession session)
        : IQueryHandler<GetAttacksQuery, GetAttacksResult>
    {
        public async Task<GetAttacksResult> Handle(GetAttacksQuery request, CancellationToken cancellationToken)
        {
            var attacks = await session.Query<Attack>().ToListAsync(cancellationToken);

            return new GetAttacksResult(attacks);
        }
    }
}
