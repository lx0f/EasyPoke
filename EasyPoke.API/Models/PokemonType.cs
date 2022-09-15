using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class PokemonType
{

    public PokemonType()
    {
        WeakTo = new HashSet<PokemonType>();
        NormalTo = new HashSet<PokemonType>();
        ResistantTo = new HashSet<PokemonType>();
        ImmuneTo = new HashSet<PokemonType>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<PokemonType> WeakTo { get; set; } = new List<PokemonType>();
    public ICollection<PokemonType> NormalTo { get; set; } = new List<PokemonType>();
    public ICollection<PokemonType> ResistantTo { get; set; } = new List<PokemonType>();
    public ICollection<PokemonType> ImmuneTo { get; set; } = new List<PokemonType>();
}
