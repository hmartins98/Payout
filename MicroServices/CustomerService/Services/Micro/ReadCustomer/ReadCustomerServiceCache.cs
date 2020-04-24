using Google.Protobuf;
using CustomerService.Repository.SQL;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.ReadCustomer
{
	public class ReadCustomerServiceCache : IServiceCache<RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>,
		Task<Customer>>
	{
		public void AddToCache(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input, Task<Customer> output)
		{
			input.CacheRepository.Set(input.Request.Id.ToString(), output.Result.ToByteArray());
			//Utilizar para testes
			//input.CacheRepository.Set("1", output.Result.ToByteArray());
		}

		public Task<Customer> GetFromCache(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input)
		{
			byte[] customerSerialized = input.CacheRepository.Get(input.Request.Id.ToString());
			//descomentar para testes
			//customerSerialized = input.CacheRepository.Get("1");

			if (customerSerialized == null || customerSerialized == default)
				return null;

			Customer Customer = new Customer();
			Customer.MergeFrom(customerSerialized);

			return Task.FromResult(Customer);
		}
	}
}
