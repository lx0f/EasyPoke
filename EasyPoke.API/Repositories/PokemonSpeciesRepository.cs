using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class PokemonSpeciesRepository : IPokemonSpeciesRepository
{
    private readonly DataContext _context;

    public PokemonSpeciesRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(PokemonSpecies pokemonSpecies)
    {
        _context.PokemonSpecies.Add(pokemonSpecies);
    }

    public PokemonSpecies? Get(int id)
    {
        return _context.PokemonSpecies.Find(id);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public IEnumerable<PokemonSpecies> GetAll()
    {
        return _context.PokemonSpecies.ToList();
    }

    public void RemoveRange(IEnumerable<PokemonSpecies> species)
    {
        _context.PokemonSpecies.RemoveRange(species);
    }
}
