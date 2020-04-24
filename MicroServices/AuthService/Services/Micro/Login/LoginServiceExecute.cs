using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class LoginServiceExecute : IServiceExecute<RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository>,
        Task<AuthenticationReply>>
    {
        public Task<AuthenticationReply> Execute(RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository> input)
        {
            try
            {
                UserAuthentication user = input.Repository.GetUserAuthenticationByUsername(input.Request.Username);

                if (user == null)
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "Invalid Login Credentials!"));

                //verify the password and if is not valid return null
                if (!PasswordSecurity.VerifyPassword(input.Request.Password, user.PasswordHash, user.PasswordSalt))
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "Invalid Login Credentials!"));

                return Task.FromResult(new AuthenticationReply()
                {
                    UserId = user.UserID,
                    ValidationToken = new CustomTypesGRPC.GUID()
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
