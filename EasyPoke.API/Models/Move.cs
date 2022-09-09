using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class Move
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Power { get; set; }
    public int PowerPoint { get; set; }
    public int Accuracy { get; set; }
}

