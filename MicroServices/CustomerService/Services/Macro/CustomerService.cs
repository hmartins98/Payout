using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using CustomerService.Repository.SQL;
using Microsoft.Extensions.Caching.Distributed;
using CustomerService.Services.CreateCustomer;
using ServiceBased.RequestMessages;
using CustomerService.Services.DeleteCustomer;
using CustomerService.Services.ReadCustomer;
using CustomerService.Services.UpdateCustomer;
using CustomTypesGRPC;

namespace CustomerService
{
    public class CustomerService : CustomerContract.CustomerContractBase
    {
        private readonly IDistributedCache _cacheRepository;
        private readonly ICustomerRepository _authenticationRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IDistributedCache cache,
            ICustomerRepository authenticationRepository,
            ILogger<CustomerService> logger)
        {
            _cacheRepository = cache;
            _authenticationRepository = authenticationRepository;
            _logger = logger;
        }

        public override Task<Customer> CreateCustomer(Customer request, ServerCallContext context)
        { 
            CreateCustomerService loginService = new CreateCustomerService();

            RequestMessageBase<Customer, ServerCallContext, ICustomerRepository> requestMessage = new RequestMessageBase<Customer, ServerCallContext, ICustomerRepository>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository
            };

            return loginService.Run(requestMessage);
        }

        public override Task<Customer> ReadCustomer(CustomerId request, ServerCallContext context)
        {
            ReadCustomerService readService = new ReadCustomerService();

            RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> requestMessage = new RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository,
                CacheRepository = _cacheRepository
            };

            return readService.Run(requestMessage);
        }

        public override Task<BOOL> UpdateCustomer(Customer request, ServerCallContext context)
        {
            UpdateCustomerService updateService = new UpdateCustomerService();

            RequestMessageBaseWithCache<Customer, ServerCallContext, ICustomerRepository, IDistributedCache> requestMessage = new RequestMessageBaseWithCache<Customer, ServerCallContext, ICustomerRepository, IDistributedCache>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository,
                CacheRepository = _cacheRepository               
            };

            return updateService.Run(requestMessage);
        }

        public override Task<BOOL> DeleteCustomer(CustomerId request, ServerCallContext context)
        {
            DeleteCustomerService deleteService = new DeleteCustomerService();

            RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> requestMessage = new RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>
            {
                Request = request,
                Context = context,
                Repository = _authenticationRepository,
                CacheRepository = _cacheRepository
            };

            return deleteService.Run(requestMessage);
        }
    }
}
