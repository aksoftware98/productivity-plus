using AKSoftware.ProductivityPlus.Server.Domain;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(AKSoftware.ProductivityPlus.Server.Api.Startup))]
namespace AKSoftware.ProductivityPlus.Server.Api
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var config = builder.GetContext().Configuration;
			builder.Services.AddAppServices(config);
		}
	}
}
