using EasyPoke.API.Data;
using EasyPoke.API.Repositories;
using EasyPoke.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultPolicy = "DefaultPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: defaultPolicy, policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors(defaultPolicy);

app.UseAuthorization();

app.MapControllers();

// Setup Pokemon data
var optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
var options = optionsBuilder.Options;
var context = new DataContext(options);

var folderPath = "/Users/andy/Personal/EasyPoke/EasyPoke.API/Data/csv/test/";
var typeFilePath = folderPath + "pokemon_types.csv";
var typeEfficacyFilePath = folderPath + "type_efficacy.csv";
var growthRateFilePath = folderPath + "growth_rates.csv";
var growthRateLevelExperienceFilePath = folderPath + "experience.csv";
var pokemonFilePath = folderPath + "pokemon_species.csv";

var importer = new PokemonDataImporter(
    context,
    typeFilePath,
    growthRateFilePath,
    growthRateLevelExperienceFilePath,
    pokemonFilePath,
    typeEfficacyFilePath);

importer.Import();
context.Dispose();

app.Run();

public partial class Program { }
