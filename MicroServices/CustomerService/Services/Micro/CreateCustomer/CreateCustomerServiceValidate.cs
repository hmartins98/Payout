using CustomerService.Repository.SQL;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;

namespace CustomerService.Services.CreateCustomer
{
	public class CreateCustomerServiceValidate : IServiceValidate<RequestMessageBase<Customer, ServerCallContext, ICustomerRepository>>
	{
		public void Validate(RequestMessageBase<Customer, ServerCallContext, ICustomerRepository> input)
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
