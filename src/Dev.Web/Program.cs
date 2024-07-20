using Dev.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure web application
builder.ConfigureWebApplication();

// Configure the HTTP request pipeline.
var app = builder.Build();
await app.ConfigureRequestPipelineAsync();
