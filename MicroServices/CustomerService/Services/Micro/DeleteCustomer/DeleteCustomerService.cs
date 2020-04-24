using CustomerService.Repository.SQL;
using CustomTypesGRPC;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.DeleteCustomer
{
    public class DeleteCustomerService
        : ServiceBaseInvalidatesCache<DeleteCustomerServiceValidate, DeleteCustomerServiceInvalidatesCache, DeleteCustomerServiceExecute,
            RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>,
            Task<BOOL>>
    {

    }
}
