using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class GrowthRate
{
    public GrowthRate()
    {
        LevelExperiences = new HashSet<GrowthRateLevelExperience>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Formula { get; set; }

    public ICollection<GrowthRateLevelExperience> LevelExperiences { get; set; }
}
