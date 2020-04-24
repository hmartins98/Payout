using CustomerService.Repository.SQL;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.ReadCustomer
{
    public class ReadCustomerService
        : ServiceBaseWithCache<ReadCustomerServiceValidate, ReadCustomerServiceCache, ReadCustomerServiceExecute,
            RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>,
            Task<Customer>>
    {

    }
}
