using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IGrowthRateRepository
{
    void Add(GrowthRate growthRate);
    GrowthRate? Get(int id);
    int Save();
}

