using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class PokemonMoveLevel
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public Move Move { get; set; }
    public Pokemon Pokemon { get; set; }
}

