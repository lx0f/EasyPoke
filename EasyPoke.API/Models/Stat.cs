using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class Stat
{
    [Key]
    public int Id { get; set; }
    public int HitPoint { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
}

