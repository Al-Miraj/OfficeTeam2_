using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
});

builder.Services.AddControllers();
builder.Services.AddTransient<IEventAttendanceService, EventAttendanceService>();
builder.Services.AddTransient<ILoginService, LoginService>(); 
var app = builder.Build();

app.Urls.Add("https://localhost:5000/");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); 
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "This is without anything");
app.Run();