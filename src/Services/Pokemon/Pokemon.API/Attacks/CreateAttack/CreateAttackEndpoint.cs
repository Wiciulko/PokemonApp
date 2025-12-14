using Carter;
using Mapster;
using MediatR;
using Pokemon.API.Enums;

namespace Pokemon.API.Attacks.CreateAttack
{
    public record CreateAttackRequest(string Name, AttackType AttackType, int Damage);
    public record CreateAttackResponse(Guid Id);

    internal class CreateAttackEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/attacks", async (CreateAttackRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateAttackCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateAttackResponse>();

                return Results.Created($"/attacks/{response.Id}", response);
            })
                .WithName("CreateAttack")
                .Produces<CreateAttackResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Attack")
                .WithDescription("Create Attack");
        }
    }
}
