using EasyPoke.API.Repositories;

namespace EasyPoke.API.Data;

public class PokemonDataImporter
{

    private readonly DataContext _context;

    public string TypeFilePath { get; set; }
    public string GrowthRateFilePath { get; set; }
    public string GrowthRateLevelExperienceFilePath { get; set; }
    public string PokemonFilePath { get; set; }

    public PokemonDataImporter(DataContext context)
    {
        _context = context;
    }

    public void Import()
    {
        // Import all Types
        // Register Type Relations
        // - Weak To
        // - Normal To
        // - Resistant To
        // - Immune to
        ImportTypes();

        // Import all Growth Rate
        // Import all Growth Rate Level Experience
        // Register all Growth Rate and Level Experience Relations
        ImportGrowthRate();

        // Import all Pokemons
        // Register all Pokemon and Pokemon Types Relations
        // Register all Pokemon and Growth Rate Relations
        ImportPokemons();
    }

    private void ImportPokemons()
    {
        throw new NotImplementedException();
    }

    private void ImportGrowthRate()
    {
        throw new NotImplementedException();
    }

    private void ImportTypes()
    {
        throw new NotImplementedException();
    }
}
