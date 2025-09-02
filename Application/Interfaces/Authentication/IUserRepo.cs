using Application.Dtos.Request.Account;
using Domain.Entities.Authentication;
using static Application.Dtos.Responses.ServiceRersponse;

namespace Application.Interfaces.Authentication
{
    public interface IUserRepo 
    {
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user);

        public Task<LoginResponse> LoginAsync(LoginModelDto loginModel);
        public Task<RegisterResponse> RegisterAsync(RegisterModelDto registerModel);
    }
}
