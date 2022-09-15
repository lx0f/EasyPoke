using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class GrowthRateRepository
{
    private readonly DataContext _context;

    public GrowthRateRepository(DataContext context)
    {
        _context = context;
    }

    public GrowthRate? GetGrowthRateById(int growthRateId)
    {
        return _context.GrowthRates.Find(growthRateId);
    }

    public void AddGrowthRate(GrowthRate growthRate)
    {
        _context.GrowthRates.Add(growthRate);
        _context.SaveChanges();
    }
}
