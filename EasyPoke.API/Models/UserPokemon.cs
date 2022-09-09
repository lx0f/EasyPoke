using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class UserPokemon
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public Stat Stat { get; set; }
    public int Experience { get; set; }
    public Pokemon Pokemon { get; set; }
}
