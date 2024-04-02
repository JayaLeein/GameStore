using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;
public static class GameEndpoints
{
    // We're never going to be reconstructing the entire list from scratch: readonly
   private static readonly List<GameDto> games = [
    new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)),
    new (
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27)),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
 {


    // changes everything to group
    // everything start with games
    // in public static WebApplication MapGamesEndpoints(this WebApplication app)
    // changed WebApplication into RouteGroupBuilder
    var group = app.MapGroup("games").WithParameterValidation();

const string GetGameEndpointName = "GetGame";

// List<GameDto> games = [
//     new (
//         1,
//         "Street Fighter II",
//         "Fighting",
//         19.99M,
//         new DateOnly(1992, 7, 15)),
//     new (
//         2,
//         "Final Fantasy XIV",
//         "Roleplaying",
//         59.99M,
//         new DateOnly(2010, 9, 30)),
//     new (
//         3,
//         "FIFA 23",
//         "Sports",
//         69.99M,
//         new DateOnly(2022, 9, 27)),
// ];

// GET /games
// What kind of verb I'm going to implement (get)
// ("") the path under which this endpoint is going to be located 
// () => specify the handler: how are you going to be handling a request that comes into that path 
// This is the minimal API, see how short it is
group.MapGet("/", () => games);

// If a request comes for the get verb into the root ("/") of my application then I'm going to reply with this '() => "Hello World!"'
// app.MapGet("/", () => "Hello World!");

// GET /games/1
group.MapGet("/{id}", (int id) => 
{
    // It means that we could receive a game or either null
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);

})
.WithName(GetGameEndpointName);


// POST /games
// When somebody post something into the game's endpoint 
group.MapPost("/", (CreateGameDto newGame) =>
{
    //Validation for name ( can not be null )
    // if (string.IsNullOrEmpty(newGame.Name))
    // {   
    //     return Results.BadRequest("Name is required");
    // }

    GameDto game = new(
        games.Count + 1, // our id
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
})
// Nuget minimal APIs extensions package, then make it to the "group"
// .WithParameterValidation();

// PUT /games
group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if(index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE /games/1
group.MapDelete("/{id}",(int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

return group;

}
}
