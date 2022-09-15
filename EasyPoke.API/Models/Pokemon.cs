using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyPoke.API.Models;

public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    [JsonIgnore]
    public User User { get; set; }
    public PokemonSpecies PokemonSpecies { get; set; }
}
