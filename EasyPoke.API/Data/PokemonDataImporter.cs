using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

namespace EasyPoke.API.Data;

public class PokemonDataImporter
{

    private readonly DataContext _context;

    private readonly string _typeFilePath;
    private readonly string _typeEfficacyFilePath;
    private readonly string _growthRateFilePath;
    private readonly string _growthRateLevelExperienceFilePath;
    private readonly string _pokemonFilePath;

    public PokemonDataImporter(DataContext context, string typeFilePath, string growthRateFilePath, string growthRateLevelExperienceFilePath, string pokemonFilePath, string typeEfficacyFilePath)
    {
        _context = context;
        _typeFilePath = typeFilePath;
        _growthRateFilePath = growthRateFilePath;
        _growthRateLevelExperienceFilePath = growthRateLevelExperienceFilePath;
        _pokemonFilePath = pokemonFilePath;
        _typeEfficacyFilePath = typeEfficacyFilePath;
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
        // import types
        using (StreamReader sr = new(_typeFilePath))
        {
            string line;
            int idIndex = 0;
            int nameIndex = 1;
            sr.ReadLine(); // Ignore first line
            while ((line = sr.ReadLine()) != null)
            {
                string[] lineData = line.Split(',');
                int id = Convert.ToInt16(lineData[idIndex]);
                string name = lineData[nameIndex];

                _context.PokemonTypes.Add(new PokemonType()
                {
                    Id = id,
                    Name = name
                });
            }
        }

        _context.SaveChanges();

        // import type efficacy
        using (StreamReader sr = new(_typeEfficacyFilePath))
        {
            string line;
            int damageIndex = 0;
            int targetIndex = 1;
            int damageFactorIndex = 2;
            sr.ReadLine(); // Ignore first line
            while ((line = sr.ReadLine()) != null)
            {
                string[] lineData = line.Split(',');

                int damageTypeId = Convert.ToInt16(lineData[damageIndex]);
                int targetTypeId = Convert.ToInt16(lineData[targetIndex]);
                int damageFactor = Convert.ToInt16(lineData[damageFactorIndex]);

                PokemonType targetType = _context.PokemonTypes.Find(targetTypeId);
                PokemonType damageType = _context.PokemonTypes.Find(damageTypeId);

                switch (damageFactor)
                {
                    case 200:
                        if (targetType.WeakTo == null)
                            targetType.WeakTo = new List<PokemonType>();
                        targetType.WeakTo.Add(damageType);
                        break;
                    case 100:
                        if (targetType.NormalTo == null)
                            targetType.NormalTo = new List<PokemonType>();
                        targetType.NormalTo.Add(damageType);
                        break;
                    case 50:
                        if (targetType.ResistantTo == null)
                            targetType.ResistantTo = new List<PokemonType>();
                        targetType.ResistantTo.Add(damageType);
                        break;
                    case 0:
                        if (targetType.ImmuneTo == null)
                            targetType.ImmuneTo = new List<PokemonType>();
                        targetType.ImmuneTo.Add(damageType);
                        break;
                }
            }

            _context.SaveChanges();
        }
    }
}
