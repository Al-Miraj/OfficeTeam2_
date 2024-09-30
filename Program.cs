using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<EventServices>();
builder.Services.AddSingleton<ILoginService, LoginService>(); //addsingleton
var app = builder.Build();

app.Urls.Add("https://localhost:5000/");
app.MapControllers();
app.MapGet("/", () => "This is without anything");
app.Run();