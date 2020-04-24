using AuthService.Repository;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace AuthService.Services.Login
{
    public class CreateAuthService
        : ServiceBase<CreateAuthServiceValidate, CreateAuthServiceExecute,
            RequestMessageBase<CreateAuthenticationRequest, ServerCallContext, IAuthenticationRepository>,
            Task<AuthenticationReply>>
    {

    }
}
