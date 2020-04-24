using Google.Protobuf;
using CustomerService.Repository.SQL;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using ServiceBased.RequestMessages;
using System.Threading.Tasks;

namespace CustomerService.Services.DeleteCustomer
{
	public class DeleteCustomerServiceInvalidatesCache : IServiceInvalidatesCache<RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache>>
	{
		public void InvalidateCache(RequestMessageBaseWithCache<CustomerId, ServerCallContext, ICustomerRepository, IDistributedCache> input)
		{
			if(input.CacheRepository.Get(input.Request.Id.ToString()) != null)
				input.CacheRepository.Remove(input.Request.Id.ToString());
		}
	}
}
