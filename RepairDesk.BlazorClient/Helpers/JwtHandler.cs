using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Net.Http.Headers;

namespace RepairDesk.BlazorClient.Helpers
{
    public class JwtHandler : DelegatingHandler
    {
		private readonly ILocalStorageService _localStorageService;
        private readonly IMemoryCache _cache;

        public JwtHandler(IMemoryCache cache, ILocalStorageService localStorageService, HttpMessageHandler innerHandler) 
            : base(innerHandler)
        {
			_localStorageService = localStorageService;
            _cache = cache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetJwtToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        public async Task SetJwtToken(string token)
        {
            _cache.Set("authToken", token, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)));
            try
            {
                await _localStorageService.SetItemAsync("authToken", token);
            }
            catch { }
        }

        public async Task RemoveJwtToken()
        {
            _cache.Remove("authToken");
            try
            {
                await _localStorageService.RemoveItemAsync("authToken");
            }
            catch { }
        }

        public async Task<string> GetJwtToken()
        {
            if (_cache.TryGetValue("authToken", out string token)){}
            //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6IkFkbWluIFVzZXIiLCJlbWFpbCI6ImFkbWluQGV4YW1wbGUuY29tIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjg2NTcyMjAyLCJleHAiOjE2ODcxNzcwMDIsImlhdCI6MTY4NjU3MjIwMn0.Teyw6ldJQqcFLaG3IicR-ro_WVR00tDXZiOYjti1dvE";
            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    token = await _localStorageService.GetItemAsync<string>("authToken");
                    _cache.Set("authToken", token, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)));
                }
                catch { }
            }

            return token;
        }
    }
}
