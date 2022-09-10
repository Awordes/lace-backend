using Lace.Application.Mapping;

namespace Lace.Application.CQRS.User.ViewModels;

public class UserViewModel: IMapFrom<Domain.Entities.User>
{
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
}