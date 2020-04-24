using CustomerService.Repository.SQL;
using CustomTypesGRPC;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.UpdateCustomer
{
    public class UpdateCustomerService
        : ServiceBaseInvalidatesCache<UpdateCustomerServiceValidate, UpdateCustomerServiceInvalidatesCache, UpdateCustomerServiceExecute,
            RequestMessageBaseWithCache<Customer, ServerCallContext, ICustomerRepository, IDistributedCache>,
            Task<BOOL>>
    {

    }
}
