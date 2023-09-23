using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain.Options
{
	public class GraphManagerOptions
	{

		public string TenantId { get; set; } = string.Empty;

		public string ClientId { get; set; } = string.Empty;

		public string ClientSecret { get; set; } = string.Empty;

		public string Scope { get; set; } = string.Empty;

	}
}
