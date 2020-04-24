using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;

namespace AuthService.Services.Login
{
	public class TokenServiceValidate : IServiceValidate<RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository>>
    {
        public void Validate(RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository> input)
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

				if (input.Request.ValidationToken == Guid.Empty)
					throw new ArgumentNullException();
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
