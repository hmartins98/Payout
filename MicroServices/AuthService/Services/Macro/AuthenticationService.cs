using System.Threading.Tasks;
using Grpc.Core;
using AuthService.Repository;
using Microsoft.Extensions.Logging;
using AuthService.Services.Login;
using ServiceBased.RequestMessages;

namespace AuthService
{
    public class AuthenticationService : Authentication.AuthenticationBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IAuthenticationRepository authentucationRepository,
            ILogger<AuthenticationService> logger)
        {
            _authenticationRepository = authentucationRepository;
            _logger = logger;
        }

        public override Task<AuthenticationReply> AuthenticateLogin(AuthenticationLoginRequest request, ServerCallContext context)
        {
            LoginService loginService = new LoginService();

            RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository> requestMessage = new RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository
            };

           return loginService.Run(requestMessage);
        }

        public override Task<AuthenticationReply> AuthenticateToken(AuthenticationTokenRequest request, ServerCallContext context)
        {
            TokenService tokenService = new TokenService();

            RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository> requestMessage = new RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository
            };

            return tokenService.Run(requestMessage);
        }

        public override Task<AuthenticationReply> CreateAuthentication(CreateAuthenticationRequest request, ServerCallContext context)
        {
            CreateAuthService createAuthService = new CreateAuthService();

            RequestMessageBase<CreateAuthenticationRequest, ServerCallContext, IAuthenticationRepository> requestMessage = new RequestMessageBase<CreateAuthenticationRequest, ServerCallContext, IAuthenticationRepository>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository
            };

            return createAuthService.Run(requestMessage);
        }
    }
}
