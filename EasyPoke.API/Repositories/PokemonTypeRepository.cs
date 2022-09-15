using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class PokemonTypeRepository : IPokemonTypeRepository
{
    private readonly DataContext _context;

    public PokemonTypeRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(PokemonType pokemonType)
    {
        _context.PokemonTypes.Add(pokemonType);
    }

    public PokemonType? Get(int id)
    {
        return _context.PokemonTypes.Find(id);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public IEnumerable<PokemonType> GetAll()
    {
        return _context.PokemonTypes.ToList();
    }

    public void RemoveRange(IEnumerable<PokemonType> types)
    {
        _context.PokemonTypes.RemoveRange(types);
    }
}
