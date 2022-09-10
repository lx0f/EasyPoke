using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class PokemonSpecies
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int HitPoint { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }

    public PokemonType Type { get; set; }
    public GrowthRate GrowthRate { get; set; }
}
