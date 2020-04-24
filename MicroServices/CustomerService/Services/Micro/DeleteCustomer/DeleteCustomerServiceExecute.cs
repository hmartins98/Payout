using CustomerService.Repository.SQL;
using CustomTypesGRPC;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Services.DeleteCustomer
{
    public class DeleteCustomerServiceExecute : IServiceExecute<RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>,
        Task<BOOL>>
    {
        public Task<BOOL> Execute(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input)
        {
            try
            {
                input.Repository.DeleteCustomer(input.Request.Id);

                return Task.FromResult(new BOOL(true));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
