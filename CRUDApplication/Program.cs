using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Uses a concurrent dictionary to make the API thread-safe
var _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/fruit", () => Fruit.All);
 
Func<string, Fruit> getFruit = id => Fruit.All[id];
app.MapGet("/fruit/{id}", getFruit);          
 
app.MapPost("/fruit/{id}", Handlers.AddFruit);
 
Handlers handlers = new();                    
app.MapPut("/fruit/{id}", handlers.ReplaceFruit);
 
app.MapDelete("/fruit/{id}", DeleteFruit);    
 
app.Run();
 
void DeleteFruit(string id)                   
{
    Fruit.All.Remove(id);
}
 
record Fruit(string Name, int Stock)
{
    public static readonly Dictionary<string, Fruit> All = new();
};
 
class Handlers
{
    public void ReplaceFruit(string id, Fruit fruit)
    {
        Fruit.All[id] = fruit;
    }
 
    public static void AddFruit(string id, Fruit fruit)
    {
        Fruit.All.Add(id, fruit);
    }
}
