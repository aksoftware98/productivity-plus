using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKSoftware.ProductivityPlus.Server.Domain.Exceptions
{
	public class InvalidInputException : Exception
	{

		public InvalidInputException(string message) : base(message)
		{

		}

	}

	public class InvalidValueException : Exception
	{

		public InvalidValueException(string message) : base(message)
		{

		}

	}

	public class DomainException : Exception
	{
		public DomainException(string message) : base(message)
		{

		}
	}
}
