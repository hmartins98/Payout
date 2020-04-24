using AuthService;
using CustomerService;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static GrpcChannel CreateAuthenticatedChannel(string address, string _token)
        {
            var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                if (!string.IsNullOrEmpty(_token))
                {
                    metadata.Add("Authorization", $"Bearer {_token}");
                }
                return Task.CompletedTask;
            });

            // SslCredentials is used here because this channel is using TLS.
            // Channels that aren't using TLS should use ChannelCredentials.Insecure instead.
            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });
            return channel;
        }

        static void Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5000");

            /*var client = new Authentication.AuthenticationClient(channel);

            CreateAuthenticationRequest request = new CreateAuthenticationRequest()
            {
                Username = "teste",
                Password = "testePass",
                Nome = "Nome Teste",
                Email = "teste@gmail.com"
            };
            var reply = client.CreateAuthentication(request);

            Console.WriteLine(reply.SessionKey);
            Console.WriteLine(reply.UserId);*/

            var client = new CustomerContract.CustomerContractClient(channel);

            CustomerId request = new CustomerId()
            {
                Id = Guid.NewGuid()
            };

            var reply = client.ReadCustomer(request);

            Console.WriteLine(reply.Id);
            Console.WriteLine(reply.Name);


            //Metadata headers = new Metadata
            //{
            //    { "AuthID", $"{reply.UserId}" },
            //    { "SessionKey", $"{reply.SessionKey}" },
            //};

            //var channel1 = CreateAuthenticatedChannel("https://localhost:5001", reply.Token);

            /*var users = new User.UserClient(channel);

            using (var reply1 = users.GetUsers(new UsersRequest(), headers))
            {
                Console.WriteLine(reply1);

                while (await reply1.ResponseStream.MoveNext())
                {
                    var user = reply1.ResponseStream.Current;

                    Console.WriteLine(user.UserId);
                    Console.WriteLine(user.Email);
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine(user.LastName);
                }
            }*/

            Console.ReadLine();
        }
    }
}
