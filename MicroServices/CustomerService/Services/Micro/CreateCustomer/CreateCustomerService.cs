using CustomerService.Repository.SQL;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.CreateCustomer
{
    public class CreateCustomerService
        : ServiceBase<CreateCustomerServiceValidate, CreateCustomerServiceExecute,
            RequestMessageBase<Customer, ServerCallContext, ICustomerRepository>,
            Task<Customer>>
    {

    }
}
