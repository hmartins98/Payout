﻿using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;

namespace AuthService.Services.Login
{
	public class LoginServiceValidate : IServiceValidate<RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository>>
    {
        public void Validate(RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository> input)
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

				if (string.IsNullOrWhiteSpace(input.Request.Password))
					throw new ArgumentNullException();

				if (string.IsNullOrWhiteSpace(input.Request.Username))
					throw new ArgumentNullException();
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
