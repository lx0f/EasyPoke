using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

namespace EasyPoke.API.Services;

public class PokemonSpeciesService : IPokemonSpeciesService
{
    private readonly IPokemonSpeciesRepository _repository;

    public PokemonSpeciesService(IPokemonSpeciesRepository repository)
    {
        _repository = repository;
    }

    public PokemonSpecies? GetSpeciesById(int id)
    {
        return _repository.Get(id);
    }
}
