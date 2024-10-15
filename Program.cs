using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<EventServices>();
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // nodig zodat session werkt
});

var app = builder.Build();

app.Urls.Add("https://localhost:5000/");
app.UseSession();  //session middleware
app.MapControllers();
app.Run();

