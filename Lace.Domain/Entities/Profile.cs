namespace Lace.Domain.Entities;

public class Profile
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }

    public ICollection<ProfileAttribute> ProfileAttributes { get; set; }
}