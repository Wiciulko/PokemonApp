using BuildingBlocks.CQRS;
using Marten;
using Marten.Pagination;
using Pokemon.API.Models;

namespace Pokemon.API.Attacks.GetAttacks
{
    public record GetAttacksQuery(int? PageNumber = 1, int? PageSize = 20) : IQuery<GetAttacksResult>;
    public record GetAttacksResult(IEnumerable<Attack> Attacks);

    internal class GetAttacksHandler(IDocumentSession session)
        : IQueryHandler<GetAttacksQuery, GetAttacksResult>
    {
        public async Task<GetAttacksResult> Handle(GetAttacksQuery query, CancellationToken cancellationToken)
        {
            var attacks = await session
                .Query<Attack>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 20, cancellationToken);

            return new GetAttacksResult(attacks);
        }
    }
}
