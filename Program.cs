var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // This registers the controllers.

// Add Swagger for API documentation (optional but useful for testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Commenting out the authentication middleware for now
// app.UseAuthentication(); // Enable authentication if needed
app.UseAuthorization();  // Enable authorization if needed

// This maps the controllers (including EventsController) to appropriate routes
app.MapControllers();

app.Run();
