// TODO: Refactor this damn importing function - ITS TOO LONG

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

    public PokemonDataImporter(
        DataContext context,
        string typeFilePath,
        string growthRateFilePath,
        string growthRateLevelExperienceFilePath,
        string pokemonFilePath,
        string typeEfficacyFilePath)
    {
        _context = context;
        _typeFilePath = typeFilePath;
        _growthRateFilePath = growthRateFilePath;
        _growthRateLevelExperienceFilePath = growthRateLevelExperienceFilePath;
        _pokemonFilePath = pokemonFilePath;
        _typeEfficacyFilePath = typeEfficacyFilePath;

        var types = _context.PokemonTypes.ToList();
        _context.PokemonTypes.RemoveRange(types);

        var growthRates = _context.GrowthRates.ToList();
        _context.GrowthRates.RemoveRange(growthRates);

        var levelExperiences = _context.GrowthRateLevelExperiences.ToList();
        _context.GrowthRateLevelExperiences.RemoveRange(levelExperiences);

        _context.SaveChanges();
    }

    public void Import()
    {
        var allTypes = _context.PokemonTypes.ToList();
        _context.PokemonTypes.RemoveRange(allTypes);
        _context.SaveChanges();

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
        // TODO: ImportPokemons();
    }

    private void ImportPokemons()
    {
        throw new NotImplementedException();
    }

    private void ImportGrowthRate()
    {
        // TODO: Refactor and reduce code dependencies
        // import growth rates
        using (StreamReader sr = new(_growthRateFilePath))
        {
            sr.ReadLine(); // Skip header

            List<string> result = new();

            int cInt;
            string buffer = "";

            bool inQuote = false;
            // TODO: Refactor this bitch
            // Lord baby jesus why did it have to be this long. I bet
            // there was an existing solution too but my lazy ass 
            // refused to google that shit tsk. Shooting myself in the 
            // foot istg. Ain't nothing SOLID about this that's for
            // sure. Talk about dodgy shady ass code... I wrote it like
            // 5 minutes ago, AND I already don't remember what 50%
            // of the shit below does
            // Signed-off-by: Luth Andyka <luthandyka.business@gmail.com>
            while ((cInt = sr.Read()) != -1)
            {
                char c = (char)cInt;
                if (c == '"')
                {
                    if (inQuote)
                    {
                        buffer += c;
                        result.Add(buffer);
                        buffer = "";
                        inQuote = false;
                        sr.Read();
                    }
                    else
                    {
                        buffer += c;
                        inQuote = true;
                    }
                }
                else if (c == ',')
                {
                    if (inQuote)
                    {
                        buffer += c;
                    }
                    else
                    {
                        result.Add(buffer);
                        buffer = "";
                    }
                }
                else if (c == '\n')
                {
                    if (inQuote)  
                    {
                        buffer += c;
                    }
                    else
                    {
                        result.Add(buffer);
                        buffer = "";
                        continue;
                    }
                }
                else
                {
                    buffer += c;
                }
            }

            for (int i = 0; i < result.Count; i += 3)
            {
                _context.GrowthRates.Add(new GrowthRate()
                {
                    Id = Convert.ToInt32(result[i]),
                    Name = result[i + 1],
                    Formula = result[i + 2]
                });
            }
        }

        _context.SaveChanges();

        // import Level Exexperiences
        using (StreamReader sr = new(_growthRateLevelExperienceFilePath))
        {
            string line;
            int growthRateIdIndex = 0;
            int levelIndex = 1;
            int experienceIndex = 2;
            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] lineData = line.Split(',');
                int growthRateId = Convert.ToInt16(lineData[growthRateIdIndex]);
                int level = Convert.ToInt16(lineData[levelIndex]);
                int experience = Convert.ToInt32(lineData[experienceIndex]);
                var growthRate = _context.GrowthRates.Find(growthRateId);

                if (growthRate.LevelExperiences == null)
                    growthRate.LevelExperiences = new List<GrowthRateLevelExperience>();

                GrowthRateLevelExperience levelExperience = new()
                {
                    Level = level,
                    Experience = experience
                };

                _context.GrowthRateLevelExperiences.Add(levelExperience);
                growthRate.LevelExperiences.Add(levelExperience);
            }

            _context.SaveChanges();
        }
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
