using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class TokenService
        : ServiceBase<TokenServiceValidate, TokenServiceExecute,
            RequestMessageBase<AuthenticationTokenRequest, ServerCallContext, IAuthenticationRepository>,
            Task<AuthenticationReply>>
    {

    }
}
