using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace RepairDesk.BlazorClient.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        //private readonly ILocalStorageService _localStorageService;
        private readonly JwtHandler _jwtHandler;

        public CustomAuthenticationStateProvider(JwtHandler jwtHandler)
        {
            //_localStorageService = localStorageService;
            _jwtHandler = jwtHandler;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _jwtHandler.GetJwtToken();
            //try
            //{
            //    token = await _localStorageService.GetItemAsync<string>("authToken");
            //}
            //catch  {}

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenClaims = handler.ReadJwtToken(token).Claims;

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(tokenClaims, "Bearer")));
            }
        }
    }

}
