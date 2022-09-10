using Lace.Application.CQRS.ProfileAttribute.ViewModels;
using Lace.Application.Mapping;

namespace Lace.Application.CQRS.Profile.ViewModels;

public class ProfileViewModel: IMapFrom<Domain.Entities.Profile>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public ICollection<ProfileAttributeViewModel> ProfileAttributes { get; set; }

    public ICollection<ProfileLaceViewModel> ProfileLaces { get; set; }
}