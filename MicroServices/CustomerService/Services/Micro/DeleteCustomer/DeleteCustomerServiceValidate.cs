using CustomerService.Repository.SQL;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System;

namespace CustomerService.Services.DeleteCustomer
{
	public class DeleteCustomerServiceValidate : IServiceValidate<RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>>
	{
		public void Validate(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input)
		{
			try
			{
				if (input == null)
					throw new ArgumentNullException();

				if (input.Context == null)
					throw new ArgumentNullException();

				if (input.Repository == null)
					throw new ArgumentNullException();

				if (input.Request == null)
					throw new ArgumentNullException();

				if (input.Request.Id == Guid.Empty)
					throw new ArgumentNullException();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
