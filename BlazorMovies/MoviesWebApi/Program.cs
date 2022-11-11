using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<MoviesWebApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesWebApiContext") ?? throw new InvalidOperationException("Connection string 'MoviesWebApiContext' not found.")));

// Add automapper capabilities 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Setup CORS rules
string politikaSlobodanLokal = "slobodno_sve_u_lokalu";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: politikaSlobodanLokal,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7206",
                                              "http://localhost:7206")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
});

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(politikaSlobodanLokal);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
