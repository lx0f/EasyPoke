using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class GrowthRateLevelExperience
{
    [Key]
    public int Id { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public GrowthRate ParentGrowthRate { get; set; }
}
