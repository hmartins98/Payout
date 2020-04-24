using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class CreateAuthServiceExecute : IServiceExecute<RequestMessageBase<CreateAuthenticationRequest, ServerCallContext, IAuthenticationRepository>,
        Task<AuthenticationReply>>
    {
        public Task<AuthenticationReply> Execute(RequestMessageBase<CreateAuthenticationRequest, ServerCallContext, IAuthenticationRepository> input)
        {
            try
            {
                if (input.Repository.GetUserAuthenticationByUsername(input.Request.Username) != null)
                    throw new Exception("Username already exists!");

                //generate the hash for the password
                PasswordSecurity.CreatePasswordHash(input.Request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                input.Repository.CreateAuthentication(input.Request.Username, passwordHash, passwordSalt);

                LoginService loginService = new LoginService();

                AuthenticationLoginRequest authenticationRequest = new AuthenticationLoginRequest()
                {
                    Username = input.Request.Username,
                    Password = input.Request.Password
                };

                RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository> requestMessage = new RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository>
                {
                    Request = authenticationRequest,
                    Context = input.Context,
                    Repository = input.Repository
                };

                return loginService.Run(requestMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
