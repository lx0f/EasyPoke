using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class GrowthRate
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Formula { get; set; }
    public ICollection<GrowthRateLevelExperience> LevelExperiences { get; set; }
}

public class GrowthRateLevelExperience
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public GrowthRate GrowthRate { get; set; }
}
