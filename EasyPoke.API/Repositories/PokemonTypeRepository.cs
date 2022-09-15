using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class PokemonTypeRepository
{
    private readonly DataContext _context;

    public PokemonTypeRepository(DataContext context)
    {
        _context = context;
    }

    public void AddType(PokemonType pokemonType)
    {
        _context.PokemonTypes.Add(pokemonType);
        _context.SaveChanges();
    }

    public PokemonType? GetTypeById(int typeId)
    {
        return _context.PokemonTypes.Find(typeId);
    }
}
