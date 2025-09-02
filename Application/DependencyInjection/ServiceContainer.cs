using Application.Configuration;
using Application.Identity;
using Application.Interfaces.Authentication;
using Application.Services.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(MapperInitializer).Assembly);
            services.AddAuthorizationCore();
            services.AddCascadingAuthenticationState();
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();

            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            services.AddScoped<IUserRepo, AccountService>();

            services.AddMudServices();



            return services;
        }
    }
}
