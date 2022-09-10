namespace Lace.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public Profile Profile { get; set; }
}