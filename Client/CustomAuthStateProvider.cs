using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());
            if(await _localStorageService.GetItemAsync<bool>("isAuth"))
            {
                var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, "Admin")
                }, "test authentication type");

                var user = new ClaimsPrincipal(identity);
                 state = new AuthenticationState(user);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;

        }
    }
}