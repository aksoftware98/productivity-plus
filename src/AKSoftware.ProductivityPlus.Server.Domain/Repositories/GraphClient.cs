using AKSoftware.ProductivityPlus.Server.Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain.Repositories
{
	public class GraphClient
	{
		private readonly ILogger<GraphClient> _logger;
		private readonly HttpClient _httpClient;
		private readonly GraphManagerOptions _options;
		public GraphClient(HttpClient httpClient,
						   GraphManagerOptions options,
						   ILogger<GraphClient> logger)
		{
			_httpClient = httpClient;
			_options = options;
			_logger = logger;
		}

		private async Task<string> GetTokenAsync()
		{
			var confidentialClient = ConfidentialClientApplicationBuilder
											.Create(_options.ClientId)
											.WithClientSecret(_options.ClientSecret)
											.WithClientId(_options.ClientId)
											.WithTenantId(_options.TenantId)
											.Build();

			var scopes = new string[] { _options.Scope };
			try
			{
				var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();
				return authResult.AccessToken;
			}
			catch (MsalClientException ex)
			{
				_logger.LogError($"Failed to fetch the token from Azure Active Directory. Error code: {ex.ErrorCode} - {ex.Message}");
				throw;
			}
		}

	}
}
