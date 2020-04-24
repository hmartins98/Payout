using CustomerService.Repository.SQL;
using Grpc.Core;
using ServiceBased.RequestMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Services.CreateCustomer
{
    public class CreateCustomerServiceExecute : IServiceExecute<RequestMessageBase<Customer, ServerCallContext, ICustomerRepository>,
        Task<Customer>>
    {
        public Task<Customer> Execute(RequestMessageBase<Customer, ServerCallContext, ICustomerRepository> input)
        {
            try
            {
                input.Repository.CreateCustomer(input.Request);

                return Task.FromResult(input.Request);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
