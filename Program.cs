using ChRobinson.CountryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Register controllers (picks up CountryController automatically).
builder.Services.AddControllers();

// Register BfsService so it can be injected into the controller.
builder.Services.AddSingleton<BfsService>();

// Allow any React dev server (or any origin) to call this API.
// Tighten WithOrigins() for production deployments.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
