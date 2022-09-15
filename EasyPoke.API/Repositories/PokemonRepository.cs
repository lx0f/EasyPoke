using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly DataContext _context;

    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
    }

    public void Delete(Pokemon pokemon)
    {
        _context.Pokemons.Remove(pokemon);
    }

    public Pokemon? Get(int id)
    {
        return _context.Pokemons.Find(id);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
