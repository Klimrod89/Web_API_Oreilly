using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddRazorPages();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.MapGet("/test", () => "Hello World");

app.MapHealthChecks("healthz");
app.MapRazorPages();

app.Run();
