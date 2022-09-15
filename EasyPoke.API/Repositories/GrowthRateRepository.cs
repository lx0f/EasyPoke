using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class GrowthRateRepository : IGrowthRateRepository
{
    private readonly DataContext _context;

    public GrowthRateRepository(DataContext context)
    {
        _context = context;
    }

    public GrowthRate? Get(int id)
    {
        return _context.GrowthRates.Find(id);
    }

    public void Add(GrowthRate growthRate)
    {
        _context.GrowthRates.Add(growthRate);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public IEnumerable<GrowthRate> GetAll()
    {
        return _context.GrowthRates.ToList();
    }

    public void RemoveRange(IEnumerable<GrowthRate> growthRates)
    {
        _context.GrowthRates.RemoveRange(growthRates);
    }
}
