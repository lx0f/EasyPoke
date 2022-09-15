using EasyPoke.API.Models;

namespace EasyPoke.API.Services;

public interface IPokemonService
{
    Pokemon? AddPokemonToUser(User user, PokemonSpecies species);
    bool DeletePokemonById(int id);
    Pokemon? GetPokemonById(int id);
}
