namespace Lace.Domain.Entities;

public class ProfileAttribute
{
    public Guid Id { get; set; }

    public Profile Profile { get; set; }

    public Category Category { get; set; }

    public DictionaryElement? DictionaryElement { get; set; }

    public string? ExternalValue { get; set; }
}