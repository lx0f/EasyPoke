using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class PokemonSpeciesRepository
{
    private readonly DataContext _context;

    public PokemonSpeciesRepository(DataContext context)
    {
        _context = context;
    }

    public void AddPokemonSpecies(PokemonSpecies pokemonSpecies)
    {
        _context.PokemonSpecies.Add(pokemonSpecies);
    }

    public PokemonSpecies? GetSpeciesById(int id)
    {
        return _context.PokemonSpecies.Find(id);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
