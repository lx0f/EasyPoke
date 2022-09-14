// TODO: Refactor this damn importing function - ITS TOO LONG

using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

namespace EasyPoke.API.Data;

public class PokemonDataImporter
{
    private readonly DataContext _context;
    private readonly string _pokemonFilePath;
    private readonly string _pokemonTypeFilePath;
    private readonly string _typeFilePath;
    private readonly string _typeEfficacyFilePath;
    private readonly string _growthRateFilePath;
    private readonly string _growthRateLevelExperienceFilePath;

    public PokemonDataImporter(
        DataContext context,
        string pokemonFilePath,
        string pokemonTypeFilePath,
        string typeFilePath,
        string typeEfficacyFilePath,
        string growthRateFilePath,
        string growthRateLevelExperienceFilePath)
    {
        _context = context;
        _typeFilePath = typeFilePath;
        _growthRateFilePath = growthRateFilePath;
        _growthRateLevelExperienceFilePath = growthRateLevelExperienceFilePath;
        _pokemonFilePath = pokemonFilePath;
        _typeEfficacyFilePath = typeEfficacyFilePath;
        _pokemonTypeFilePath = pokemonTypeFilePath;
    }

    public void Import()
    {
        RemoveAll();

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

    private void RemoveAll()
    {
        var types = _context.PokemonTypes.ToList();
        _context.PokemonTypes.RemoveRange(types);

        var growthRates = _context.GrowthRates.ToList();
        _context.GrowthRates.RemoveRange(growthRates);

        var levelExperiences = _context.GrowthRateLevelExperiences.ToList();
        _context.GrowthRateLevelExperiences.RemoveRange(levelExperiences);

        var pokemonSpecies = _context.PokemonSpecies.ToList();
        _context.PokemonSpecies.RemoveRange(pokemonSpecies);

        _context.SaveChanges();

    }

    private void ImportPokemons()
    {
        int idIndex = 0;
        int nameIndex = 1;
        int evolveFromIdIndex = 2;
        int growthRateIdIndex = 3;
        int hitPointIndex = 4;
        int attackIndex = 5;
        int defenseIndex = 6;
        int spAttackIndex = 7;
        int spDefenseIndex = 8;
        int speedIndex = 9;

        // Add pokemons
        List<List<string>> pokemonSpeciesData;
        using (StreamReader sr = new StreamReader(_pokemonFilePath))
        {
            CsvReader cr = new CsvReader(sr, 10, true);
            pokemonSpeciesData = cr.Read();
        }

        foreach (List<string> pokemonData in pokemonSpeciesData)
        {
            int id = Convert.ToInt16(pokemonData[idIndex]);
            string name = pokemonData[nameIndex];
            int hitPoint = Convert.ToInt16(pokemonData[hitPointIndex]);
            int attack = Convert.ToInt16(pokemonData[attackIndex]);
            int defense = Convert.ToInt16(pokemonData[defenseIndex]);
            int spAttack = Convert.ToInt16(pokemonData[spAttackIndex]);
            int spDefense = Convert.ToInt16(pokemonData[spDefenseIndex]);
            int speed = Convert.ToInt16(pokemonData[speedIndex]);

            // TODO: change to pokemonspecies repository
            _context.PokemonSpecies.Add(new PokemonSpecies()
            {
                Id = id,
                Name = name,
                HitPoint = hitPoint,
                Attack = attack,
                Defense = defense,
                SpecialAttack = spAttack,
                SpecialDefense = spDefense,
                Speed = speed
            });
        }

        // Add pokemon evolve from
        foreach (List<string> pokemonData in pokemonSpeciesData)
        {
            string evolveFromIdString = pokemonData[evolveFromIdIndex];

            if (evolveFromIdString.Length == 0)
                continue;

            int id = Convert.ToInt16(pokemonData[idIndex]);
            int evolveFromId = Convert.ToInt16(evolveFromIdString);

            PokemonSpecies pokemonSpecies = _context.PokemonSpecies.Find(id);
            PokemonSpecies evolveFrom = _context.PokemonSpecies.Find(evolveFromId);

            pokemonSpecies.EvolvedFrom = evolveFrom;
        }

        // Add pokemon growth rates
        foreach (List<string> pokemonData in pokemonSpeciesData)
        {
            int growthRateId = Convert.ToInt16(pokemonData[growthRateIdIndex]);
            int pokemonSpeciesId = Convert.ToInt16(pokemonData[idIndex]);

            GrowthRate growthRate = _context.GrowthRates.Find(growthRateId);
            PokemonSpecies pokemonSpecies = _context.PokemonSpecies.Find(pokemonSpeciesId);

            pokemonSpecies.GrowthRate = growthRate;
        }

        // Add pokemon their types
        int typeIdIndex = 1;
        int slotIndex = 2;

        List<List<string>> pokemonTypeData;
        using (StreamReader sr = new StreamReader(_pokemonTypeFilePath))
        {
            CsvReader cr = new CsvReader(sr, 3, true);
            pokemonTypeData = cr.Read();
        }

        foreach (List<string> pokemonData in pokemonTypeData)
        {
            int id = Convert.ToInt16(pokemonData[idIndex]);
            int typeId = Convert.ToInt16(pokemonData[typeIdIndex]);
            int slot = Convert.ToInt16(pokemonData[slotIndex]);

            PokemonSpecies pokemonSpecies = _context.PokemonSpecies.Find(id);
            PokemonType pokemonType = _context.PokemonTypes.Find(typeId);

            if (slot == 1)
            {
                pokemonSpecies.Type1 = pokemonType;
            }
            else if (slot == 2)
            {
                pokemonSpecies.Type2 = pokemonType;
            }
        }

        _context.SaveChanges();
    }

    private void ImportGrowthRate()
    {
        List<List<string>> growthRateDataList;
        using (StreamReader sr = new StreamReader(_growthRateFilePath))
        {
            CsvReader reader = new CsvReader(sr, 3, true);
            growthRateDataList = reader.Read();
        }

        List<List<string>> levelExperienceDataList;
        using (StreamReader sr = new StreamReader(_growthRateLevelExperienceFilePath))
        {
            CsvReader reader = new CsvReader(sr, 3, true);
            levelExperienceDataList = reader.Read();
        }

        int idIndex = 0;
        int nameIndex = 1;
        int formulaIndex = 2;
        foreach (List<string> growthRateData in growthRateDataList)
        {
            int id = Convert.ToInt16(growthRateData[idIndex]);
            string name = growthRateData[nameIndex];
            string formula = growthRateData[formulaIndex];

            _context.GrowthRates.Add(new GrowthRate()
            {
                Id = id,
                Name = name,
                Formula = formula
            });
        }

        _context.SaveChanges();

        int levelIndex = 1;
        int experienceIndex = 2;
        foreach (List<string> levelExperienceData in levelExperienceDataList)
        {
            int growthId = Convert.ToInt16(levelExperienceData[idIndex]);
            int level = Convert.ToInt16(levelExperienceData[levelIndex]);
            int experience = Convert.ToInt32(levelExperienceData[experienceIndex]);

            GrowthRate growthRate = _context.GrowthRates.Find(growthId);

            if (growthRate.LevelExperiences is null)
                growthRate.LevelExperiences = new List<GrowthRateLevelExperience>();

            growthRate.LevelExperiences.Add(new GrowthRateLevelExperience()
            {
                Level = level,
                Experience = experience
            });
        }

        _context.SaveChanges();
    }

    private void ImportTypes()
    {
        List<List<string>> typeDataList;
        using (StreamReader sr = new StreamReader(_typeFilePath))
        {
            CsvReader reader = new CsvReader(sr, 2, true);
            typeDataList = reader.Read();
        }


        List<List<string>> typeEfficacyDataList;
        using (StreamReader sr = new StreamReader(_typeEfficacyFilePath))
        {
            CsvReader reader = new CsvReader(sr, 3, true);
            typeEfficacyDataList = reader.Read();
        }

        int idIndex = 0;
        int nameIndex = 1;

        foreach (List<string> typeData in typeDataList)
        {
            int id = Convert.ToInt16(typeData[idIndex]);
            string name = typeData[nameIndex];

            _context.PokemonTypes.Add(new PokemonType()
            {
                Id = id,
                Name = name
            });
        }

        _context.SaveChanges();

        int damageTypeIdIndex = 0;
        int targetTypeIdIndex = 1;
        int damageFactorIndex = 2;

        foreach (List<string> typeEfficacyData in typeEfficacyDataList)
        {
            int damageTypeId = Convert.ToInt16(damageTypeIdIndex);
            int targetTypeId = Convert.ToInt16(targetTypeIdIndex);
            int damageFactor = Convert.ToInt16(damageFactorIndex);

            PokemonType damageType = _context.PokemonTypes.Find(damageTypeId);
            PokemonType targetType = _context.PokemonTypes.Find(targetTypeId);

            if (targetType.ImmuneTo == null)
                targetType.ImmuneTo = new List<PokemonType>();

            if (targetType.ResistantTo == null)
                targetType.ResistantTo = new List<PokemonType>();

            if (targetType.NormalTo == null)
                targetType.NormalTo = new List<PokemonType>();

            if (targetType.WeakTo == null)
                targetType.WeakTo = new List<PokemonType>();

            switch (damageFactor)
            {
                case 0:
                    targetType.ImmuneTo.Add(damageType);
                    break;
                case 50:
                    targetType.ResistantTo.Add(damageType);
                    break;
                case 100:
                    targetType.NormalTo.Add(damageType);
                    break;
                case 200:
                    targetType.WeakTo.Add(damageType);
                    break;
            }
        }
        _context.SaveChanges();
    }
}
