using System;
using System.Threading.Tasks;
using AuthService;
using Grpc.Core;
using GrpcProxy.Attributes;
using Microsoft.Extensions.Caching.Distributed;

namespace GrpcProxy
{
    public class AuthenticationEndPoint : Authentication.AuthenticationBase
    {
        /// <summary>
        /// Session repository to create new sessions after successful login
        /// </summary>
        private readonly IDistributedCache _cacheRepository;
        /// <summary>
        /// Service of authentication
        /// </summary>
        private readonly Authentication.AuthenticationClient _authentication;

        public AuthenticationEndPoint(IDistributedCache cacheRepository,
            Authentication.AuthenticationClient authentication)
        {
            _cacheRepository = cacheRepository;
            _authentication = authentication;
        }

        [AllowAnonymous]
        public override Task<AuthenticationReply> AuthenticateLogin(AuthenticationLoginRequest request, ServerCallContext context)
        {
            //comunicate with auth service to authenticate user
            AuthenticationReply reply = _authentication.AuthenticateLogin(request);

            //if authentication successful create new session for this user
            _cacheRepository.Set(reply.UserId.ToString(), ((Guid)reply.SessionKey).ToByteArray());

            //reply auth credentials
            return Task.FromResult(reply);
        }

        [AllowAnonymous]
        public override Task<AuthenticationReply> AuthenticateToken(AuthenticationTokenRequest request, ServerCallContext context)
        {
            //comunicate with auth service to authenticate user
            AuthenticationReply reply = _authentication.AuthenticateToken(request);

            //if authentication successful create new session for this user
            _cacheRepository.Set(reply.UserId.ToString(), ((Guid)reply.SessionKey).ToByteArray());

            //reply auth credentials
            return Task.FromResult(reply);
        }

        /// <summary>
        /// Register a new User authentication
        /// </summary>
        /// <param name="request">Credentials and info to create user authentications</param>
        /// <param name="context"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task<AuthenticationReply> CreateAuthentication(CreateAuthenticationRequest request, ServerCallContext context)
        {
            //comunicate with auth service to register and authenticate user
            AuthenticationReply reply = _authentication.CreateAuthentication(request);

            //if register successful create new session for this user
            _cacheRepository.Set(reply.UserId.ToString(), ((Guid)reply.SessionKey).ToByteArray());

            //reply auth credentials
            return Task.FromResult(reply);
        }
    }
}
