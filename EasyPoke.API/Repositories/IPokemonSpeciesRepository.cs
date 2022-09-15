using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IPokemonSpeciesRepository
{
    void Add(PokemonSpecies pokemonSpecies);
    PokemonSpecies? Get(int id);
    int SaveChanges();
}

