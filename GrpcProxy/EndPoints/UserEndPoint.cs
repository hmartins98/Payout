using System.Threading.Tasks;
using CustomerService;
using CustomTypesGRPC;
using Grpc.Core;

namespace GrpcProxy
{
    public class CustomerEndPoint : CustomerContract.CustomerContractBase
    {
        private readonly CustomerContract.CustomerContractClient _CustomerClient;

        public CustomerEndPoint(CustomerContract.CustomerContractClient CustomerClient)
        {
            _CustomerClient = CustomerClient;
        }

        public override Task<Customer> CreateCustomer(Customer request, ServerCallContext context)
        {
            Customer reply = _CustomerClient.CreateCustomer(request);

            return Task.FromResult(reply);
        }

        public override Task<Customer> ReadCustomer(CustomerId request, ServerCallContext context)
        {
            Customer reply = _CustomerClient.ReadCustomer(request);

            return Task.FromResult(reply);
        }

        public override Task<BOOL> UpdateCustomer(Customer request, ServerCallContext context)
        {
            BOOL reply = _CustomerClient.UpdateCustomer(request);

            return Task.FromResult(reply);
        }

        public override Task<BOOL> DeleteCustomer(CustomerId request, ServerCallContext context)
        {
            BOOL reply = _CustomerClient.DeleteCustomer(request);

            return Task.FromResult(reply);
        }
    }
}
