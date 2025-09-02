using Domain.Entities.Authentication;

namespace Application.Interfaces.Authentication
{
    public interface IJwTokenGenerator
    {
      Task<string> GenerateToken(ApplicationUser user);
    }
}
