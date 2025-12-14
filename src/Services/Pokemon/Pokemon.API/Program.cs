using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using Carter;
using FluentValidation;
using Marten;
using Pokemon.API.Attacks.CreateAttack;
using Pokemon.API.Attacks.GetAttacks;
using Pokemon.API.Pokemons.CreatePokemon;
using Pokemon.API.Pokemons.DeletePokemon;
using Pokemon.API.Pokemons.GetPokemonByAttackId;
using Pokemon.API.Pokemons.GetPokemonById;
using Pokemon.API.Pokemons.GetPokemons;
using Pokemon.API.Pokemons.UpdatePokemon;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter(null, config =>
{
    config.WithModule<GetPokemonsEndpoint>();
    config.WithModule<GetPokemonByIdEndpoint>();
    config.WithModule<GetPokemonsByAttackIdEndpoint>();
    config.WithModule<CreatePokemonEndpoint>();
    config.WithModule<UpdatePokemonEndpoint>();
    config.WithModule<DeletePokemonEndpoint>();

    config.WithModule<GetAttacksEndpoint>();
    config.WithModule<CreateAttackEndpoint>();
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
