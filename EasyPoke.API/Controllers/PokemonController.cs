using EasyPoke.API.Auth;
using EasyPoke.API.Models;
using EasyPoke.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoke.API.Controllers;

[ApiKeyAuth]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPokemonSpeciesService _pokemonSpeciesService;
    private readonly IPokemonService _pokemonService;

    public PokemonController(IUserService userService, IPokemonSpeciesService pokemonSpecies, IPokemonService pokemonService)
    {
        _userService = userService;
        _pokemonSpeciesService = pokemonSpecies;
        _pokemonService = pokemonService;
    }

    [HttpGet("user/{id}", Name = "GetUserPokemonsById")]
    public IActionResult GetUserPokemonsById(int id)
    {
        User? user = _userService.GetUserByIdIncludePokemons(id);

        if (user is null)
            return NotFound($"User of Id:{id} is not found.");

        return Ok(user.Pokemons);
    }

    [HttpPost("{id}", Name = "AddPokemonToUser")]
    public IActionResult AddPokemonToUser(int id, [FromBody] int userId)
    {
        User? user = _userService.GetUserById(userId);

        if (user is null)
            return NotFound($"User of Id:{userId} is not found.");

        PokemonSpecies? species = _pokemonSpeciesService.GetSpeciesById(id);

        if (species is null)
            return NotFound($"Pokemon species of Id:{id} is not found.");

        Pokemon? pokemon = _pokemonService.AddPokemonToUser(user, species);

        if (pokemon is null)
            return BadRequest();

        return CreatedAtAction(nameof(AddPokemonToUser), pokemon);
    }

    [HttpGet("{id}", Name = "GetPokemonById")]
    public IActionResult GetPokemonById(int id)
    {
        Pokemon? pokemon = _pokemonService.GetPokemonById(id);

        if (pokemon is null)
            return NotFound($"Pokemon of Id:{id} is not found.");

        return Ok(pokemon);
    }

    [HttpDelete("{id}", Name = "DeletePokemonById")]
    public IActionResult DeletePokemonById(int id)
    {
        bool result = _pokemonService.DeletePokemonById(id);

        if (!result)
            return NotFound($"Pokemon of Id:{id} is not found.");

        return Ok();
    }
}

