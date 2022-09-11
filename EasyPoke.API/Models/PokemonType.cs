using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class PokemonType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<PokemonType> WeakTo { get; set; }
    public ICollection<PokemonType> NormalTo { get; set; }
    public ICollection<PokemonType> ResistantTo { get; set; }
    public ICollection<PokemonType> ImmuneTo { get; set; }
}
