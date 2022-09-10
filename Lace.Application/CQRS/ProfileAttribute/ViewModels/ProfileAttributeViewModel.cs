using Lace.Application.CQRS.DictionaryElement.ViewModels;
using Lace.Application.Mapping;

namespace Lace.Application.CQRS.ProfileAttribute.ViewModels;

public class ProfileAttributeViewModel: IMapFrom<Domain.Entities.ProfileAttribute>
{
    public Guid Id { get; set; }

    public DictionaryElementViewModel DictionaryElement { get; set; }

    public string? ExternalValue { get; set; }
}