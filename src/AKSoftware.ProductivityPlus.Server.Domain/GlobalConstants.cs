using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain
{
	/// <summary>
	/// Global constants used in the Domain and the API projects
	/// </summary>
	public class GlobalConstants
	{

		public class Exceptions
		{
			/// <summary>
			/// Error code when Azure Active Directory B2C submits an an supported payload through the B2C Api Connectors
			/// </summary>
			public const string AzureB2CUnsupportedPayload = "AZURE_B2C_UNSUPPORTED_PAYLOAD";
		}

	}
}
