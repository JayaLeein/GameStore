// These are the code that boot the application
// Whole purpose: Create an instance of this "WebApplication" ( the host of your application )
// Host: to represent an HTTP server implementation for your app so that you can start listening for HTTP request
// It also stands up a bunch of components and services

using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Here can always add Builder. in these two lines to configure a series of services

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();

