using System.IO;
using System.Net;
using System.Threading.Tasks;
using AKSoftware.ProductivityPlus.Server.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AKSoftware.ProductivityPlus.Server.Api.User
{
    public class CompleteProfile
    {
        private readonly ILogger<CompleteProfile> _logger;
        private readonly IUserProfilesRepository _userProfilesRepo;
		public CompleteProfile(ILogger<CompleteProfile> log, 
                               IUserProfilesRepository userProfilesRepo)
		{
			_logger = log;
			_userProfilesRepo = userProfilesRepo;
		}

		[FunctionName("CompleteProfile")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            var userId = "1805cfc4-3a31-4bf8-802d-9060c383ef29"; // TODO: Fetch from the token

			_logger.LogInformation("Complete Profile Executing");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

