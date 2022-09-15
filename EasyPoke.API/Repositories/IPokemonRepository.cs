using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IPokemonRepository
{
    void Add(Pokemon pokemon);
    void Delete(Pokemon pokemon);
    Pokemon? Get(int id);
    void Save();
}
