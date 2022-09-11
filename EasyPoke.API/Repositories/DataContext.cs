using EasyPoke.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyPoke.API.Repositories;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<PokemonType> PokemonTypes { get; set; }
}
