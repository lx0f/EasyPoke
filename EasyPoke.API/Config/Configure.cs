using EasyPoke.API.Data;
using EasyPoke.API.Repositories;
using EasyPoke.API.Services;
using Microsoft.EntityFrameworkCore;

namespace EasyPoke.API.Config;

public static class Configure
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        // Add Cors
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin();
            });
        });

        // Add Entity Framework DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));
        });

        // Add repositories
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPokemonTypeRepository, PokemonTypeRepository>();
        services.AddTransient<IPokemonSpeciesRepository, PokemonSpeciesRepository>();
        services.AddTransient<IPokemonRepository, PokemonRepository>();
        services.AddTransient<IGrowthRateRepository, GrowthRateRepository>();

        // Add services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPokemonSpeciesService, PokemonSpeciesService>();
        services.AddTransient<IPokemonService, PokemonService>();

        // Add controllers
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void ConfigurePokemonData(DataContext context)
    {
        var folder = "/Users/andy/Personal/EasyPoke/EasyPoke.API/Data/csv/test/";
        var pokemonFilePath = folder + "pokemon_species.csv";
        var pokemonTypeFilePath = folder + "pokemon_types.csv";
        var typeFilePath = folder + "types.csv";
        var typeEfficacyFilePath = folder + "type_efficacy.csv";
        var growthRateFilePath = folder + "growth_rates.csv";
        var growthRateLevelExperienceFilePath = folder + "experience.csv";
        var importer = new PokemonDataImporter(
                context,
                pokemonFilePath,
                pokemonTypeFilePath,
                typeFilePath,
                typeEfficacyFilePath,
                growthRateFilePath,
                growthRateLevelExperienceFilePath);
        importer.Import();
    }
}
