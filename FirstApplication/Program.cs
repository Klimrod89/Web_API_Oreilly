using System.Globalization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Registration of services to IServiceCollection
builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);
// Configuration of services already in IConfiguration (Logging)
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    
}

//app.UseRouting();
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

var people = new List<Person>
{
    new("Tom", "Hanks"),
    new("Denzel", "Washington"),
    new("Leonardo", "DiCaprio"),
    new("Al", "Pacino"),
    new("Morgan", "Freeman"),
    new("Mohamed", "Chitou")
};


app.MapGet("/", () => "Hello World!");
app.MapGet("/person", () => new Person("Yassir", "Chitou"));
app.MapGet("/error", () => "Sorry, an error occurred");
TextInfo myTI = new CultureInfo("en-US",false).TextInfo;
//app.MapGet("/person/{name}", (string name) => people.Where(p => p.FirstName.StartsWith(myTI.ToTitleCase(name))));
app.MapGet("/person/{name}", (string name) => people.Where(p => p.FirstName.StartsWith(name)));






app.Run();

public record Person(string FirstName, string LastName);