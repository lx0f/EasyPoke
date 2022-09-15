using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

public class LevelExperienceRepository
{
    private readonly DataContext _context;

    public LevelExperienceRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<GrowthRateLevelExperience> GetAll()
    {
        return _context.GrowthRateLevelExperiences.ToList();
    }

    public void RemoveRange(IEnumerable<GrowthRateLevelExperience> levelExperiences)
    {
        _context.GrowthRateLevelExperiences.RemoveRange(levelExperiences);
    }
}
