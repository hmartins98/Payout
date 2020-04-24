using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class TokenServiceExecute : IServiceExecute<RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository>,
        Task<AuthenticationReply>>
    {
        public Task<AuthenticationReply> Execute(RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository> input)
        {
            try
            {
                UserAuthentication user = input.Repository.GetUserAuthenticationByToken(input.Request.ValidationToken);

                if (user == null)
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "Invalid Login Credentials!"));

                return Task.FromResult(new AuthenticationReply()
                {
                    UserId = user.UserID,
                    ValidationToken = user.ValidationToken
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
