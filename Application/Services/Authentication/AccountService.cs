using Application.Dtos.Request.Account;
using Application.Interfaces.Authentication;
using Domain.Entities.Authentication;
using System.Net.Http.Json;
using static Application.Dtos.Responses.ServiceRersponse;

namespace Application.Services.Authentication
{
   

    public class AccountService(HttpClient httpClient) : IUserRepo
    {
        public Task<ApplicationUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByPhoneNumberAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailUniqueAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameUniqueAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> LoginAsync(LoginModelDto loginModel)
        {
            var response = await httpClient.PostAsJsonAsync("api/user/login", loginModel);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }

       
        public async Task<RegisterResponse> RegisterAsync(RegisterModelDto registerModel)
        {
            var response = await httpClient.PostAsJsonAsync("api/user/register", registerModel);
            var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
            return result!;
        }

       
    }
}
