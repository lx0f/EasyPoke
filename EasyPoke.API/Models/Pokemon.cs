using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int BaseExperience { get; set; }
    public Stat BaseStat { get; set; }
    public GrowthRate GrowthRate { get; set; }
    public PokemonType Type { get; set; }
    public ICollection<PokemonMoveLevel> MoveSet { get; set; }
}
