using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AKSoftware.ProductivityPlus.Server.Domain.Exceptions;
using AKSoftware.ProductivityPlus.Server.Domain.Interfaces;
using AKSoftware.ProductivityPlus.Server.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AKSoftware.ProductivityPlus.Server.Api.User
{
	public class CreateProfile
	{
		private readonly ILogger<CreateProfile> _logger;
		private readonly IUserProfilesRepository _userProfilesRepository;
		private readonly IConfiguration _config;
		public CreateProfile(ILogger<CreateProfile> log, 
							 IUserProfilesRepository userProfilesRepository, 
							 IConfiguration config)
		{
			_logger = log;
			_userProfilesRepository = userProfilesRepository;
			_config = config;
		}

		[FunctionName("ProfileActivation")]
		[OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
		[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "profiles/activate")] HttpRequest req)
		{
			_logger.LogInformation("Profile activation executed");

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			var payload = JsonSerializer.Deserialize<AzureAdConnectorPayload>(requestBody);
			if (payload == null || !payload.Validate())
			{
				throw new InvalidDataException("AZURE_B2C_UNSUPPORTED_PAYLOAD");
			}

			_logger.LogInformation($"Profile activation step: {payload.Step}");
			bool isComplete = true;
			if (!payload.IsRegistration)
			{
				// Check if the user is existing in the database 
				var userProfile = await _userProfilesRepository.GetByUserIdAsync(payload.UserId);
				if (userProfile == null)
				{
					// Create the profile 
					userProfile = UserProfile.Create(payload.UserId,
												  payload.FirstName,
												  payload.LastName,
												  payload.Email,
												  payload.DisplayName,
												  _config["Profiles:DefaultAvatarUrl"]);
					isComplete = false;
					await _userProfilesRepository.CreateAsync(payload.UserId, userProfile);
					_logger.LogInformation($"New user '{userProfile.DisplayName}' with the ID {userProfile.Id} has just been created");
				}

				isComplete = userProfile.IsComplete;
			}

			return new OkObjectResult(new
			{
				isComplete
			});
		}

		/// <summary>
		/// The payload submitted by the Azure Active Directory B2C API connectors for the registration process and the pre-token issuance process
		/// </summary>
		class AzureAdConnectorPayload
		{
			[JsonPropertyName("step")]
			public string Step { get; set; }

			[JsonPropertyName("email")]
			public string Email { get; set; }

			[JsonPropertyName("givenName")]
			public string FirstName { get; set; }

			[JsonPropertyName("surname")]
			public string LastName { get; set; }

			[JsonPropertyName("displayName")]
			public string DisplayName { get; set; }

			[JsonPropertyName("objectId")]
			public string UserId { get; set; }

			public bool IsRegistration => Step == "PostAttributeCollection";

			public bool Validate()
			{
				if (Step != "PostAttributeCollection" && Step != "PreTokenIssuance")
					return false;
				return true;
			}

		}
	}
}

