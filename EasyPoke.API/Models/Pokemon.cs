using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public PokemonSpecies PokemonSpecies { get; set; }
    public User User { get; set; }
}
