using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YamlDotNet.Core.Tokens;

namespace RepairDesk.WpfClient.Helpers
{
    public class JwtHandler : DelegatingHandler
    {
        public JwtHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var mainViewModel = ((App)Application.Current).MainViewModel;
            if (mainViewModel != null && mainViewModel.Token != null && !string.IsNullOrEmpty(mainViewModel.Token.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", mainViewModel.Token.Token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
