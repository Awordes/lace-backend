using Lace.Application.Mapping;

namespace Lace.Application.CQRS.DictionaryElement.ViewModels;

public class DictionaryElementViewModel: IMapFrom<Domain.Entities.DictionaryElement>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}