using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RepairDesk.ApiClient;
using RepairDesk.BlazorClient.Data;
using RepairDesk.BlazorClient.Helpers;
using RepairDesk.BlazorClient.Services;
using Stripe;
using System.Net.Http.Headers;

namespace RepairDesk.BlazorClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
			builder.Services.AddServerSideBlazor();

			StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

			builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection(nameof(ApiConfig)));

            builder.Services.AddSingleton(x =>
            {
                var apiConfig = x.GetRequiredService<IOptions<ApiConfig>>().Value;
                return apiConfig.RepairDeskApiBaseAddress;
            });

			// services
			builder.Services.AddSingleton<CountdownService>();
			builder.Services.AddScoped<PaginationService>();
            builder.Services.AddScoped<ProductSearchService>();
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IMemoryCache>(factory => new MemoryCache(new MemoryCacheOptions()));


			builder.Services.AddScoped<JwtHandler>(services =>
            {
				var localStorage = services.GetRequiredService<ILocalStorageService>();
                var memoryCache = services.GetRequiredService<IMemoryCache>();
                var innerHandler = new HttpClientHandler();
                return new JwtHandler(memoryCache, localStorage, innerHandler);
            });

            builder.Services.AddScoped<IRepairDeskApiClient, RepairDeskApiClient>(services =>
            {
                var baseUrl = services.GetRequiredService<string>();
                var jwtHandler = services.GetRequiredService<JwtHandler>();
                var httpClient = new HttpClient(jwtHandler)
                {
                    BaseAddress = new Uri(baseUrl),
                };
                return new RepairDeskApiClient(baseUrl, httpClient);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

			app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}