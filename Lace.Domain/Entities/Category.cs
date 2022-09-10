namespace Lace.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<DictionaryElement> DictionaryElements { get; set; }

    public ICollection<ProfileAttribute> ProfileAttributes { get; set; }
}