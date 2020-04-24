using CustomerService.Repository.SQL;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Services.ReadCustomer
{
    public class ReadCustomerServiceExecute : IServiceExecute<RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>,
        Task<Customer>>
    {
        public Task<Customer> Execute(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input)
        {
            try
            {
                Customer customer = input.Repository.GetCustomerByID(input.Request.Id);

                if (customer == null || customer == default)
                    return null;

                return Task.FromResult(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
