using Lace.Application.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lace.Application.CQRS.User.Commands;

public class CreateUserCommand: IRequest
{
    public string Login { get; set; }

    public string Password { get; set; }
    
    public string ProfileName { get; set; }

    public string ProfileSurname { get; set; }
    
    private class Handler: IRequestHandler<CreateUserCommand>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IDbContextFactory<LaceDbContext> _contextFactory;

        public Handler(ILogger<Handler> logger, IDbContextFactory<LaceDbContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

                var user = new Domain.Entities.User
                {
                    Login = request.Login,
                    Password = request.Password
                };

                var profile = new Domain.Entities.Profile
                {
                    User = user,
                    Name = request.ProfileName,
                    Surname = request.ProfileSurname
                };

                user.Profile = profile;

                context.Users.Add(user);
                context.Profiles.Add(profile);

                await context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}