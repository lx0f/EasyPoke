using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IPokemonTypeRepository
{
    void Add(PokemonType pokemonType);
    PokemonType? Get(int id);
    int Save();
}

