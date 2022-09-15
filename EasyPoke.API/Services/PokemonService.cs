using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

namespace EasyPoke.API.Services;

public class PokemonService : IPokemonService
{
    private readonly IUserRepository _userRepository;
    private readonly IPokemonSpeciesRepository _pokemonSpeciesRepository;
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IUserRepository userRepository, IPokemonSpeciesRepository pokemonSpeciesRepository, IPokemonRepository pokemonRepository)
    {
        _userRepository = userRepository;
        _pokemonSpeciesRepository = pokemonSpeciesRepository;
        _pokemonRepository = pokemonRepository;
    }

    public Pokemon? AddPokemonToUser(User user, PokemonSpecies species)
    {
        Pokemon pokemon = new Pokemon()
        {
            Level = 1,
            Experience = 0,
            PokemonSpecies = species,
            User = user
        };

        _pokemonRepository.Add(pokemon);
        _pokemonRepository.Save();

        return pokemon;
    }

    public bool DeletePokemonById(int id)
    {
        Pokemon? pokemon = _pokemonRepository.Get(id);

        if (pokemon is null)
            return false;

        _pokemonRepository.Delete(pokemon);
        _pokemonRepository.Save();

        return true;
    }

    public Pokemon? GetPokemonById(int id)
    {
        return _pokemonRepository.Get(id);
    }
}
