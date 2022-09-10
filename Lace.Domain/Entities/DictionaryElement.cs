namespace Lace.Domain.Entities;

public class DictionaryElement
{
    public Guid Id { get; set; }

    public Category Category { get; set; }

    public string Name { get; set; }

    public ICollection<ProfileAttribute> ProfileAttributes { get; set; }
}