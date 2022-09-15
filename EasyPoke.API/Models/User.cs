using System.ComponentModel.DataAnnotations;

namespace EasyPoke.API.Models;

public class User
{
    public User()
    {
        Pokemons = new HashSet<Pokemon>();
    }

    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Pokemon> Pokemons { get; set; }
}
