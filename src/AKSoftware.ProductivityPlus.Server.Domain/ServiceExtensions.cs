using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain;

public static class ServiceExtensions
{

	public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
	{
		return services.AddSingleton(sp => new CosmosClient(config["CosmosDB:ConnectionString"], new()
		{
			AllowBulkExecution = true
		}));
	}

}