using Blazored.LocalStorage;
using RepairDesk.ApiClient;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace RepairDesk.BlazorClient.Helpers
{
    //public class AuthenticatedRepairDeskApiClient : RepairDeskApiClient
    //{
    //    public AuthenticatedRepairDeskApiClient(string baseUrl, ILocalStorageService localStorage, HttpClient httpClient)
    //        : base(baseUrl, httpClient)
    //    {
    //        //try
    //        //{
    //        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken(localStorage).Result);
    //        //}
    //        //catch(Exception ex)
    //        //{
    //        //    Debug.WriteLine(ex);
    //        //}
    //    }

    //    private async Task<string> GetToken(ILocalStorageService localStorage)
    //    {
    //        return await localStorage.GetItemAsync<string>("authToken");
    //    }
    //}
}
