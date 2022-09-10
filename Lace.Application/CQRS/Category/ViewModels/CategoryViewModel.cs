using Lace.Application.CQRS.DictionaryElement.ViewModels;
using Lace.Application.Mapping;

namespace Lace.Application.CQRS.Category.ViewModels;

public class CategoryViewModel: IMapFrom<Domain.Entities.Category>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<DictionaryElementViewModel> DictionaryElements { get; set; }
}