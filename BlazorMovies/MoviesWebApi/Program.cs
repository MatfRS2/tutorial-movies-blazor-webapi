using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesWebApi.Commands.Zanrovi;
using MoviesWebApi.Data;
using MoviesWebApi.Shared;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// read dbProvider
var dbProvider = builder.Configuration["DbProvider"];
// Add database context
if (dbProvider == "MsSql")
    builder.Services.AddDbContext<MoviesWebApiContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesWebApiContextMsSql")
            ?? throw new InvalidOperationException("Connection string 'MoviesWebApiContextMsSql' not found."));
    });
else if (dbProvider == "MySql")
    builder.Services.AddDbContext<MoviesWebApiContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("MoviesWebApiContextMySql")
            ?? throw new InvalidOperationException("Connection string 'MoviesWebApiContextMySql' not found.")));
else
    throw new InvalidOperationException("Invalid Database provider");


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

// Add MediatR capabilities
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(ZanrDodajCommandHandler).Assembly);
builder.Services.AddMediatR(MoviesWebApi.Shared.AssemblyReference.Assembly);

// Add services to the container
builder.Services
    .AddControllers(options =>
        {
            options.RespectBrowserAcceptHeader = true;
        });

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data, if necessary
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
