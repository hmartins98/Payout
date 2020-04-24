using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class LoginService
        : ServiceBase<LoginServiceValidate, LoginServiceExecute,
            RequestMessageBase<AuthenticationLoginRequest, ServerCallContext, IAuthenticationRepository>,
            Task<AuthenticationReply>>
    {

    }
}
