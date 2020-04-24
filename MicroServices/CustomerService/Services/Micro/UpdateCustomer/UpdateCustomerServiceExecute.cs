using CustomerService.Repository.SQL;
using CustomTypesGRPC;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Services.UpdateCustomer
{
    public class UpdateCustomerServiceExecute : IServiceExecute<RequestMessageBaseWithCache<Customer, ServerCallContext, ICustomerRepository, IDistributedCache>,
        Task<BOOL>>
    {
        public Task<BOOL> Execute(RequestMessageBaseWithCache<Customer, ServerCallContext, ICustomerRepository, IDistributedCache> input)
        {
            try
            {
                input.Repository.UpdateCustomer(input.Request);

                return Task.FromResult(new BOOL(true));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
