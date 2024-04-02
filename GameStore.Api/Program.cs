// These are the code that boot the application
// Whole purpose: Create an instance of this "WebApplication" ( the host of your application )
// Host: to represent an HTTP server implementation for your app so that you can start listening for HTTP request
// It also stands up a bunch of components and services

using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Here can always add Builder. in these two lines to configure a series of services

var app = builder.Build();

List<GameDto> games = [
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

// GET /games
// What kind of verb I'm going to implement (get)
// ("") the path under which this endpoint is going to be located 
// () => specify the handler: how are you going to be handling a request that comes into that path 
// This is the minimal API, see how short it is
app.MapGet("games", () => games);

// If a request comes for the get verb into the root ("/") of my application then I'm going to reply with this '() => "Hello World!"'
// app.MapGet("/", () => "Hello World!");

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));


app.Run();

