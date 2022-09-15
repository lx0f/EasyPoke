using EasyPoke.API.Models;

namespace EasyPoke.API.Services;

public interface IPokemonSpeciesService
{
    PokemonSpecies? GetSpeciesById(int id);
}
